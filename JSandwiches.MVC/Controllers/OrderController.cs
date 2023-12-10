using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.DTO.OrderDTO;
using JSandwiches.MVC.IRepository;
using JSandwiches.MVC.Models;
using JSandwiches.MVC.Models.ViewModels;
using JSandwiches.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JSandwiches.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IConsumUnitOfWork _unitOfWork;

        public OrderController(IConsumUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Listing()
        {
            var lstMenuItems = await _unitOfWork.MenuItem.GetAll();
            var baseUrl = "https://localhost:44356/images/";
            foreach (var deal in lstMenuItems)
            {
                deal.ImagePath = baseUrl + deal.ImagePath;
            }
            return View(lstMenuItems);
        }
        
        public async Task<IActionResult> ItemDetails(int id)
        {
            var menuItem = await _unitOfWork.MenuItem.GetById(id);
            if (menuItem.Item1 != null)
            {

                string fullPath = menuItem.Item1.ImagePath.ToString();

                // Get the dominant color
                var dominantColor = ColourHelper.GetDominantColor(fullPath);

                // Convert the Rgba32 object to a hex string
                string dominantColorHex = $"#{dominantColor.R:X2}{dominantColor.G:X2}{dominantColor.B:X2}";

                // Assign the hex string to the ViewBag to use in your view
                ViewBag.DominantColor = dominantColorHex;

                var baseUrl = "https://localhost:44356/images/";
                menuItem.Item1.ImagePath = baseUrl + menuItem.Item1.ImagePath;

                return View(menuItem.Item1);
            }

            var statusCode = menuItem.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        public async Task<IActionResult> OrderItem(int id)
        {
            var menuItem = await _unitOfWork.MenuItem.GetById(id);
            var lstAddOns = await _unitOfWork.AddOn.GetAll();
            var vm = new OrderVM()
            {
                MenuItemAddOn = new MenuItemAddOnDTO()
                {
                    MenuItem = menuItem.Item1
                },
                Order = new OrderDTO(),
                LstAddOnsCheckBox = lstAddOns.Select(x => new AddOnCheckBox()
                {
                    AddOn = x,
                    IsChecked = false
                }).ToList(),
                ExistInCart = false
            };
            var baseUrl = "https://localhost:44356/images/";

            vm.MenuItemAddOn.MenuItem.ImagePath = baseUrl + vm.MenuItemAddOn.MenuItem.ImagePath;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> OrderItem(OrderVM vm)
        {
            //calculating price
            decimal addOnTotal = 0; 

            foreach(var id in vm.SelectedAddOns)
            {
                var addOn = (await _unitOfWork.AddOn.GetById(Convert.ToInt32(id))).Item1;
                addOnTotal += addOn.Price;
            }
            var menuItem = await _unitOfWork.MenuItem.GetById(vm.MenuItemAddOn.MenuItem.Id);
            vm.Order.Price = (menuItem.Item1.Price * vm.Order.Amount) + addOnTotal;
            vm.Order.Amount = vm.Order.Amount;
            vm.Order.OrderStatusID = 1;

            var response = await _unitOfWork.Order.Create2(vm.Order);

            //collect list of selected task completed
            vm.LstMenuItem = new List<MenuItemAddOnDTO>();

            foreach (var id in vm.SelectedAddOns)
            {
                var menuItemAddOn = new MenuItemAddOnDTO();
                menuItemAddOn.MenuItemID = vm.MenuItemAddOn.MenuItem.Id;
                menuItemAddOn.AddOnID = Convert.ToInt32(id);
                menuItemAddOn.OrderID = response.Item2.Id;
                vm.LstMenuItem.Add(menuItemAddOn);
            }

            // saves the tasks to database
            foreach (var item in vm.LstMenuItem)
            {
                var response2 = await _unitOfWork.MenuItemAddOn.Create(item);

            }


            List<ShopCart> lstItems = new List<ShopCart>();

            if (HttpContext.Session.Get<IEnumerable<ShopCart>>(AppConst.Cart) != null
               && HttpContext.Session.Get<IEnumerable<ShopCart>>(AppConst.Cart).Count() > 0)
            {
                lstItems = HttpContext.Session.Get<List<ShopCart>>(AppConst.Cart);
            }

            foreach (var item in lstItems)
            {
                if (item.OrderID == response.Item2.Id)
                {
                    vm.ExistInCart = true;
                }
            }
            if (vm.ExistInCart != true)
            {
                var cart = new ShopCart() { OrderID = response.Item2.Id };

                lstItems.Add(cart);
                HttpContext.Session.Set(AppConst.Cart, lstItems);
            }

            return RedirectToAction("CheckOut", "CheckOut");

        }




    }
}
