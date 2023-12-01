using JSandwiches.Models.SpecialFeaturesDTO;
using JSandwiches.MVC.IRespository;
using JSandwiches.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.MVC.Controllers
{
    public class LoyaltyPointController : Controller
    {
        private readonly IConsumUnitOfWork _unitOfWork;

        public LoyaltyPointController(IConsumUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int pg)
        {
            var lstLoyaltyPoints = await _unitOfWork.LoyaltyPoint.GetAll();

            if (lstLoyaltyPoints == null)
                return RedirectToAction("ErrorPage", "Home");

            var dLstLoyaltyPoints = lstLoyaltyPoints.OrderByDescending(x => x.Id).ToList();
            var data = PagerHelper<LoyaltyPointDTO>.Paging(dLstLoyaltyPoints, pg, 4);

            ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var loyaltyPoint = new LoyaltyPointDTO();
            return View(loyaltyPoint);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoyaltyPointDTO model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ErrorPage", "Home");

            var status = await _unitOfWork.LoyaltyPoint.Create(model);
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
            var loyaltyPoint = await _unitOfWork.LoyaltyPoint.GetById(id);
            if (loyaltyPoint.Item1 != null)
                return View(loyaltyPoint.Item1);

            var statusCode = loyaltyPoint.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LoyaltyPointDTO model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ErrorPage", "Home");

            var status = await _unitOfWork.LoyaltyPoint.Update(model, model.Id);
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
            var status = await _unitOfWork.LoyaltyPoint.Delete(id);
            if (status == true)
                return RedirectToAction("Index");
            return RedirectToAction();
        }

    }
}
