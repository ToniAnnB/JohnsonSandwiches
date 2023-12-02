﻿using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.MVC.IRespository;
using JSandwiches.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IConsumUnitOfWork _unitOfWork;

        public CategoryController(IConsumUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int pg)
        {
            var lstCategories = await _unitOfWork.ItemCategory.GetAll();

            if (lstCategories == null)
                return RedirectToAction("ErrorPage", "Home");

            var dLstCategories = lstCategories.OrderByDescending(x => x.Id).ToList();
            var data = PagerHelper<ItemCategoryDTO>.Paging(dLstCategories, pg, 4);

            ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var category = new ItemCategoryDTO();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemCategoryDTO model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ErrorPage", "Home");

            var status = await _unitOfWork.ItemCategory.Create(model);
            if (status == true)
            {
                TempData["PostResponse"] = "Success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["PostResponse"] = "Failed";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _unitOfWork.ItemCategory.GetById(id);
            if (category.Item1 != null)
                return View(category.Item1);

            var statusCode = category.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemCategoryDTO model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ErrorPage", "Home");

            var status = await _unitOfWork.ItemCategory.Update(model, model.Id);
            if (status == true)
            {
                TempData["PostResponse"] = "Success2";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["PostResponse"] = "Failed";
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _unitOfWork.ItemCategory.Delete(id);
            if (status == true)
                return RedirectToAction("Index");
            return RedirectToAction();
        }

    }
}
