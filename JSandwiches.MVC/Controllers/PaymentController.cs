using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.MVC.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
