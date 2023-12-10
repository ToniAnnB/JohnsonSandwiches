using JSandwiches.Models.DTO.SpecialFeaturesDTO;
using JSandwiches.MVC.IRepository;
using JSandwiches.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.MVC.Controllers
{
    public class DealController : Controller
    {
        private readonly IConsumUnitOfWork _unitOfWork;
        private readonly HttpClient _client;

        public DealController(IConsumUnitOfWork unitOfWork, IHttpClientFactory factoryClient)
        {
            _unitOfWork = unitOfWork;
            _client = factoryClient.CreateClient("AuthAPI");
        }

        public async Task<IActionResult> Index(int pg)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var lstDeals = await _unitOfWork.Deal.GetAll();

            if (lstDeals == null)
                return RedirectToAction("ErrorPage", "Home");

            var dLstDeals = lstDeals.OrderByDescending(x => x.Id).ToList();
            var data = PagerHelper<DealDTO>.Paging(dLstDeals, pg, 4);

            ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var deal = new DealDTO();
            return View(deal);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DealDTO model)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            if (!ModelState.IsValid)
                return RedirectToAction("ErrorPage", "Home");

            var status = await _unitOfWork.Deal.Create(model);
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
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            var deal = await _unitOfWork.Deal.GetById(id);
            if (deal.Item1 != null)
                return View(deal.Item1);

            var statusCode = deal.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DealDTO model)
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");

            if (!ModelState.IsValid)
                return RedirectToAction("ErrorPage", "Home");

            var status = await _unitOfWork.Deal.Update(model, model.Id);
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
            var status = await _unitOfWork.Deal.Delete(id);
            if (status == true)
                return RedirectToAction("Index");
            return RedirectToAction();
        }

    }
}
