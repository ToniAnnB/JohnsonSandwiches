using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.MVC.IRespository;
using JSandwiches.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.MVC.Controllers
{
    public class AddOnController : Controller
    {
        private readonly IConsumUnitOfWork _unitOfWork;

        public AddOnController(IConsumUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int pg)
        {
            var lstAddOns = await _unitOfWork.AddOn.GetAll();

            if (lstAddOns == null)
                return RedirectToAction("ErrorPage", "Home");

            var dLstAddOns = lstAddOns.OrderByDescending(x => x.Id).ToList();
            var data = PagerHelper<AddOnDTO>.Paging(dLstAddOns, pg, 4);

            ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }

        public async Task<IActionResult> Details(int id)
        {
            var addOn = await _unitOfWork.AddOn.GetById(id);
            if (addOn.Item1 != null)
                return View(addOn.Item1);

            var statusCode = addOn.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var addOn = new AddOnDTO();
            return View(addOn);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddOnDTO model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ErrorPage", "Home");

            var status = await _unitOfWork.AddOn.Create(model);
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
            var addOn = await _unitOfWork.AddOn.GetById(id);
            if (addOn.Item1 != null)
                return View(addOn.Item1);

            var statusCode = addOn.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddOnDTO model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ErrorPage", "Home");

            var status = await _unitOfWork.AddOn.Update(model, model.Id);
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
            var status = await _unitOfWork.AddOn.Delete(id);
            if (status == true)
                return RedirectToAction("Index");
            return RedirectToAction();
        }

    }
}
