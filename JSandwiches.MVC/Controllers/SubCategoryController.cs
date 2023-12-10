using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.MVC.IRepository;
using JSandwiches.MVC.Models;
using JSandwiches.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JSandwiches.MVC.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly IConsumUnitOfWork _unitOfWork;
        private readonly HttpClient _client;

        public SubCategoryController(IConsumUnitOfWork unitOfWork, IHttpClientFactory factoryClient)
        {
            _unitOfWork = unitOfWork;
            _client = factoryClient.CreateClient("AuthAPI");
        }

        public async Task<IActionResult> Index(int pg)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var lstsubCategories = await _unitOfWork.ItemSubCategory.GetAll();

            if (lstsubCategories == null)
                return RedirectToAction("ErrorPage", "Home");

            var dLstsubCategories = lstsubCategories.OrderByDescending(x => x.Id).ToList();
            var data = PagerHelper<ItemSubCategoryDTO>.Paging(dLstsubCategories, pg, 4);

            ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var subCategory = new SubCategoryVM()
            {
                SubCategory = new ItemSubCategoryDTO(),
                ddlCategory = await GetCategoriessDDL()
            };
            return View(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryVM vm)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var status = await _unitOfWork.ItemSubCategory.Create(vm.SubCategory);
            if (status == true)
            {
                TempData["PostResponse"] = "Success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["PostResponse"] = "Failed";
                vm.ddlCategory = await GetCategoriessDDL();
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var subCategory = await _unitOfWork.ItemSubCategory.GetById(id);
            if (subCategory.Item1 != null)
            {
                var vm = new SubCategoryVM()
                {
                    SubCategory = subCategory.Item1,
                    ddlCategory = await GetCategoriessDDL()
                };
                return View(vm);
            }

            var statusCode = subCategory.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SubCategoryVM vm)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var status = await _unitOfWork.ItemSubCategory.Update(vm.SubCategory, vm.SubCategory.Id);
            if (status == true)
            {
                TempData["PostResponse"] = "Success2";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["PostResponse"] = "Failed";
                vm.ddlCategory = await GetCategoriessDDL();
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _unitOfWork.ItemSubCategory.Delete(id);
            if (status == true)
                return RedirectToAction("Index");
            return RedirectToAction();
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriessDDL()
        {
            var lstCategories = await _unitOfWork.ItemCategory.GetAll();

            var ddlCategories = lstCategories.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()
            });

            return ddlCategories;
        }

    }
}
