using JSandwiches.Models.SpecialFeaturesDTO;
using JSandwiches.MVC.IRespository;
using JSandwiches.MVC.Models;
using JSandwiches.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace JSandwiches.MVC.Controllers
{
    public class DealSpecificsController : Controller
    {
        private readonly IConsumUnitOfWork _unitOfWork;

        public DealSpecificsController(IConsumUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int pg)
        {
            var lstDealSpecificss = await _unitOfWork.DealSpecifics.GetAll();

            if (lstDealSpecificss == null)
                return RedirectToAction("ErrorPage", "Home");

            var dLstDealSpecificss = lstDealSpecificss.OrderByDescending(x => x.Id).ToList();
            var data = PagerHelper<DealSpecificsDTO>.Paging(dLstDealSpecificss, pg, 4);

            ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dealSpecifics = await _unitOfWork.DealSpecifics.GetById(id);
            if (dealSpecifics.Item1 != null)
                return View(dealSpecifics.Item1);

            var statusCode = dealSpecifics.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var vm = new DealSpecificsVM()
            {
                DealSpecifics = new DealSpecificsDTO(),
                ddlDeals = await GetDealsDDL()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DealSpecificsVM vm)
        {
            var imageFile = "";

            if (vm.DealImagePath!=null)
                imageFile = await ImageHelper.SaveImage(vm.DealImagePath);

            if(imageFile!=null)
            vm.DealSpecifics.ImagePath = imageFile;


            var status = await _unitOfWork.DealSpecifics.Create(vm.DealSpecifics);
            if (status == true)
            {
                TempData["PostResponse"] = "Success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["PostResponse"] = "Failed";
                vm.ddlDeals = await GetDealsDDL();
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var dealSpecifics = await _unitOfWork.DealSpecifics.GetById(id);
            if (dealSpecifics.Item1 != null)
            {
                var vm = new DealSpecificsVM()
                {
                    DealSpecifics = dealSpecifics.Item1,
                    ddlDeals = await GetDealsDDL()
                };
                return View(vm);
            }

            var statusCode = dealSpecifics.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DealSpecificsVM vm)
        {
            //var nImagePah = vm.DealSpecifics.ImagePath.Replace( @"\\", @"backslash");
           // mvc\server\uploads\dd0de68e - 9960 - 4265 - 898b - 56f1dea10397_summerheat.png

            var status = await _unitOfWork.DealSpecifics.Update(vm.DealSpecifics, vm.DealSpecifics.Id);
            if (status == true)
            {
                TempData["PostResponse"] = "Success2";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["PostResponse"] = "Failed";
                vm.ddlDeals = await GetDealsDDL();
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _unitOfWork.DealSpecifics.Delete(id);
            if (status == true)
                return RedirectToAction("Index");
            return RedirectToAction();
        }

        public async Task<IEnumerable<SelectListItem>> GetDealsDDL()
        {
            var lstDeals = await _unitOfWork.Deal.GetAll();

            var ddlDeals = lstDeals.Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()
            });

            return ddlDeals;
        }
    }
}
