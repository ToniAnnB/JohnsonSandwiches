using JSandwiches.MVC.IRepository;
using JSandwiches.MVC.Models;
using JSandwiches.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JSandwiches.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConsumUnitOfWork _unitOfWork;
        private readonly HttpClient _client;

        public HomeController(IConsumUnitOfWork unitOfWork, IHttpClientFactory factoryClient, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _client = factoryClient.CreateClient("AuthAPI");
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var access = AuthorizationHelper.IsAuthenticated(_client, HttpContext);
            if (access == false)
                return RedirectToAction("Index", "Login");


            //gathering summary information
            var lstPayments = await _unitOfWork.Payment.GetAll();
            decimal mTotal = 0;
            decimal dTotal = 0;

            var todayDate = DateTime.Now;
            var monthlyOrders = lstPayments.Where(x => x.PaymentDate.Month == todayDate.Month && x.PaymentDate.Year == todayDate.Year).ToList();
            foreach (var item in monthlyOrders)
            {
                mTotal += item.TotalCost;
            }
            var todayOrders = lstPayments.Where(x => x.PaymentDate.Month == todayDate.Month && x.PaymentDate.Year == todayDate.Year && x.PaymentDate.Day == todayDate.Day).ToList();

            foreach (var item in todayOrders)
            {
                dTotal += item.TotalCost;
            }


            var lstOrders = await _unitOfWork.MenuItemAddOn.GetAll();
            var lstMenuItem = lstOrders.GroupBy(x => x.MenuItemID)
                                    .Select(group => new { Record = group.Key, Count = group.Count() })
                                    .OrderByDescending(x => x.Count);

            var menuItemID = lstMenuItem.First().Record;
            var menuItem = await _unitOfWork.MenuItem.GetById(menuItemID);
            var menuItemTitle = menuItem.Item1.Title;

            //creating actual model
            var vm = new SummaryVM()
            {
                numMonthlyRevenue = monthlyOrders.Count(),
                MonthlyRevenue = mTotal,
                numDailyRevenue = todayOrders.Count(),
                DailyRevenue = dTotal,
                TopSeller = menuItemTitle
            };

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
