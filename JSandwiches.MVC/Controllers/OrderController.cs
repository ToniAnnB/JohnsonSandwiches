using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.DTO.OrderDTO;
using JSandwiches.MVC.IRespository;
using JSandwiches.MVC.Models;
using JSandwiches.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var vm = new OrderVM()
            {
                Order = new OrderDTO()
                {
                    MenuItemAddOn = new MenuItemAddOnDTO()
                    {
                        MenuItem = menuItem.Item1
                    },
                    
                },

            };
            return View();
        }

        //public JsonResult CalculateTotalPrice(List<int> selectedCheckboxIds)
        //{
        //    Implement your calculation logic based on selectedCheckboxIds
        //    Replace this with your actual logic
        //    var totalPrice = YourCalculationMethod(selectedCheckboxIds);

        //    return Json(new { totalPrice });
        //}


    }
}
