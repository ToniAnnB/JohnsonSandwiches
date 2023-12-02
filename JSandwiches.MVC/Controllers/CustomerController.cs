using JSandwiches.Models.DTO.UsersDTO;
using JSandwiches.MVC.IRespository;
using JSandwiches.MVC.Models;
using JSandwiches.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JSandwiches.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IConsumUnitOfWork _unitOfWork;

        public CustomerController(IConsumUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int pg)
        {
            var lstCustomers = await _unitOfWork.CustomerAddress.GetAll();

            if (lstCustomers == null)
                return RedirectToAction("ErrorPage", "Home");

            var dLstCustomers = lstCustomers.OrderByDescending(x => x.Id).ToList();
            var data = PagerHelper<CustomerAddressDTO>.Paging(dLstCustomers, pg, 4);

            ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }

        public async Task<IActionResult> Details(int id)
        {

            var menuItem = await _unitOfWork.CustomerAddress.GetById(id);
            if (menuItem.Item1 != null)
                return View(menuItem.Item1);


            var statusCode = menuItem.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var vm = new CustomerVM()
            {
                Customer = new CustomerDTO(),
                Address = new AddressDTO(),
                ddlParishes = await GetParishDDL()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerVM vm)
        {
            vm.CustomerAddress = new CustomerAddressDTO();
            var status = await _unitOfWork.Customer.Create2(vm.Customer);
            var status2 = await _unitOfWork.Address.Create2(vm.Address);
            if (status.Item1 == true && status2.Item1 == true)
            {
                vm.CustomerAddress.CustomerID = status.Item2.Id;
                vm.CustomerAddress.AddressID = status2.Item2.Id;
                var status3 = await _unitOfWork.CustomerAddress.Create(vm.CustomerAddress);
                if (status3 == true)
                {
                    TempData["PostResponse"] = "Success";
                    return RedirectToAction("Index");
                }
            }

            TempData["PostResponse"] = "Failed";
            vm.ddlParishes = await GetParishDDL();
            return View(vm);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var dealSpecifics = await _unitOfWork.CustomerAddress.GetById(id);
            if (dealSpecifics.Item1 != null)
            {
                var vm = new CustomerVM()
                {
                    Customer = dealSpecifics.Item1.Customer,
                    Address = dealSpecifics.Item1.Address,
                    CustomerAddress = dealSpecifics.Item1,
                    ddlParishes = await GetParishDDL()
                };
                return View(vm);
            }

            var statusCode = dealSpecifics.Item2;
            if (statusCode == "404")
                return RedirectToAction("NotFound", "Home");
            return RedirectToAction("ErrorPage", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerVM vm)
        {
            var status = await _unitOfWork.Customer.Update(vm.Customer, vm.Customer.Id);
            var status2 = await _unitOfWork.Address.Update(vm.Address, vm.Address.Id);
            if (status == true && status2 == true)
            {
                TempData["PostResponse"] = "Success2";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["PostResponse"] = "Failed";
                vm.ddlParishes = await GetParishDDL();
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _unitOfWork.CustomerAddress.Delete(id);
            if (status == true)
                return RedirectToAction("Index");
            return RedirectToAction();
        }

        public async Task<IEnumerable<SelectListItem>> GetParishDDL()
        {
            var lstParishes = await _unitOfWork.Parish.GetAll();

            var ddlParishes = lstParishes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return ddlParishes;
        }

    }
}
