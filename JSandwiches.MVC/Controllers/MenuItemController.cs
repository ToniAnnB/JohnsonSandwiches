using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.MVC.IRepository;
using JSandwiches.MVC.Models;
using JSandwiches.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;


namespace JSandwiches.MVC.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly IConsumUnitOfWork _unitOfWork;
        private readonly HttpClient _client;

        public MenuItemController(IConsumUnitOfWork unitOfWork, IHttpClientFactory factoryClient)
        {
            _unitOfWork = unitOfWork;
            _client = factoryClient.CreateClient("AuthAPI");
        }

        public async Task<IActionResult> Index(int pg)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var lstMenuItems = await _unitOfWork.MenuItem.GetAll();

            if (lstMenuItems == null)
                return RedirectToAction("ErrorPage", "Home");

            var dLstMenuItems = lstMenuItems.OrderByDescending(x => x.Id).ToList();
            var data = PagerHelper<MenuItemDTO>.Paging(dLstMenuItems, pg, 4);

            ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }

        public async Task<IActionResult> Details(int id)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

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

                string fontColor = ColourHelper.GetFontColor(dominantColor);

                // Pass the font color to the view
                ViewBag.FontColor = fontColor;

                var baseUrl = "https://localhost:44356/images/";
                menuItem.Item1.ImagePath = baseUrl + menuItem.Item1.ImagePath;

                return View(menuItem.Item1);
            }

            var statusCode = menuItem.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var vm = new MenuItemVM()
            {
                MenuItem = new MenuItemDTO(),
                ddlSubCategory = await GetSubCategoryDDL()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuItemVM vm)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var imageFile = "";

            if (vm.MenuItemImagePath != null)
                imageFile = await ImageHelper.SaveImage(vm.MenuItemImagePath);

            if (imageFile != null)
                vm.MenuItem.ImagePath = imageFile;


            var status = await _unitOfWork.MenuItem.Create(vm.MenuItem);
            if (status == true)
            {
                TempData["PostResponse"] = "Success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["PostResponse"] = "Failed";
                vm.ddlSubCategory = await GetSubCategoryDDL();
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var menuItem = await _unitOfWork.MenuItem.GetById(id);
            if (menuItem.Item1 != null)
            {
                var vm = new MenuItemVM()
                {
                    MenuItem = menuItem.Item1,
                    ddlSubCategory = await GetSubCategoryDDL()
                };
                return View(vm);
            }

            var statusCode = menuItem.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuItemVM vm)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var status = await _unitOfWork.MenuItem.Update(vm.MenuItem, vm.MenuItem.Id);
            if (status == true)
            {
                TempData["PostResponse"] = "Success2";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["PostResponse"] = "Failed";
                vm.ddlSubCategory = await GetSubCategoryDDL();
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _unitOfWork.MenuItem.Delete(id);
            if (status == true)
                return RedirectToAction("Index");
            return RedirectToAction();
        }

        public async Task<IEnumerable<SelectListItem>> GetSubCategoryDDL()
        {
            var lstSubCategories = await _unitOfWork.ItemSubCategory.GetAll();

            var ddlSubCategories = lstSubCategories.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()
            });

            return ddlSubCategories;
        }
    }

}

