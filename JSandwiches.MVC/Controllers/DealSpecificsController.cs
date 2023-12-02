﻿using JSandwiches.Models.DTO.SpecialFeaturesDTO;
using JSandwiches.MVC.IRespository;
using JSandwiches.MVC.Models;
using JSandwiches.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            var baseUrl = "https://localhost:44356/images/";
            foreach (var deal in data.Item1)
            {
                deal.ImagePath = baseUrl + deal.ImagePath;
            }
            return View(data.Item1);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var vm = new DealSpecificsVM()
            {
                DealSpecificsDTO = new DealSpecificsDTO(),
                ddlDeals = await GetDealsDDL()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DealSpecificsVM vm)
        {
            var imageFile = "";

            if (vm.DealImagePath != null)
                imageFile = await ImageHelper.SaveImage(vm.DealImagePath);

            if (imageFile != null)
                vm.DealSpecificsDTO.ImagePath = imageFile;


            var status = await _unitOfWork.DealSpecifics.Create(vm.DealSpecificsDTO);
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
                    DealSpecificsDTO = dealSpecifics.Item1,
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
            //var nImagePah = vm.DealSpecificsDTO.ImagePath.Replace( @"\\", @"backslash");
            // mvc\server\uploads\dd0de68e - 9960 - 4265 - 898b - 56f1dea10397_summerheat.png

            var status = await _unitOfWork.DealSpecifics.Update(vm.DealSpecificsDTO, vm.DealSpecificsDTO.Id);
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
