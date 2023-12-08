using Azure.Core;
using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.DTO.OrderDTO;
using JSandwiches.Models.Food;
using JSandwiches.Models.Order;
using JSandwiches.MVC.IRespository;
using JSandwiches.MVC.Models;
using JSandwiches.MVC.Models.ViewModels;
using JSandwiches.MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace JSandwiches.MVC.Controllers
{
    [IgnoreAntiforgeryToken]
    public class CheckOutController : Controller
    {
        private readonly IConsumUnitOfWork _unitOfWork;
        public string PayPalClientId { get; set; } = null;
        private string PayPalSecret { get; set; } = null;
        public string PayPalUrl { get; set; } = null;


        public CheckOutController(IConfiguration configuration, IConsumUnitOfWork unitOfWork)
        {
            PayPalClientId = configuration["PayPalSettings:ClientId"]!;
            PayPalSecret = configuration["PayPalSettings:Secret"]!;
            PayPalUrl = configuration["PayPalSettings:Url"]!;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> CheckOut()
        {
            List<ShopCart> lstItems = new List<ShopCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopCart>>(AppConst.Cart) != null
                && HttpContext.Session.Get<IEnumerable<ShopCart>>(AppConst.Cart).Count() > 0)
            {
                lstItems = HttpContext.Session.Get<List<ShopCart>>(AppConst.Cart);
            }

            //list of order ids
            List<int> lstCartOrderID = lstItems.Select(x => x.OrderID).ToList();

            List<PaymentVM> lstVM = new List<PaymentVM>();
            var lstMenuItemAddOn = (await _unitOfWork.MenuItemAddOn.GetAll()).ToList();
            var lstOrder = (await _unitOfWork.Order.GetAll()).ToList();

            //list of selected orders associated with customer session
            var lstCartOrder = new List<OrderDTO>();
            foreach (var order in lstOrder)
            {
                foreach (var id in lstCartOrderID)
                {
                    if (order.Id == id)
                        lstCartOrder.Add(order);
                }
            }

            //payment details being gathered
            foreach (var order in lstCartOrder)
            {
                var lstSelectedItems = lstMenuItemAddOn.Where(x => x.OrderID == order.Id).ToList();
                var vm = new PaymentVM()
                {
                    Order = order,
                    MenuItem = lstSelectedItems.Select(x => x.MenuItem).FirstOrDefault(),
                    lstAddOns = lstSelectedItems.Select(x => x.AddOn).ToList()
                };
                var baseUrl = "https://localhost:44356/images/";
                vm.MenuItem.ImagePath = baseUrl + vm.MenuItem.ImagePath;
                lstVM.Add(vm);
            }

            ViewBag.PayPalClientId = PayPalClientId;
            ViewBag.PayPalSecret = PayPalSecret;
            ViewBag.PayPalUrl = PayPalUrl;

            return View(lstVM);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrder()
        {
            List<ShopCart> lstItems = new List<ShopCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopCart>>(AppConst.Cart) != null
                && HttpContext.Session.Get<IEnumerable<ShopCart>>(AppConst.Cart).Count() > 0)
            {
                lstItems = HttpContext.Session.Get<List<ShopCart>>(AppConst.Cart);
            }

            //list of order ids
            List<int> lstCartOrderID = lstItems.Select(x => x.OrderID).ToList();

            List<PaymentVM> lstVM = new List<PaymentVM>();
            var lstMenuItemAddOn = (await _unitOfWork.MenuItemAddOn.GetAll()).ToList();
            var lstOrder = (await _unitOfWork.Order.GetAll()).ToList();

            //list of selected orders associated with customer session
            var lstCartOrder = new List<OrderDTO>();
            foreach (var order in lstOrder)
            {
                foreach (var id in lstCartOrderID)
                {
                    if (order.Id == id)
                        lstCartOrder.Add(order);
                }
            }

            decimal totalCost = 0;
            //payment details being gathered
            foreach (var order in lstCartOrder)
            {
                var lstSelectedItems = lstMenuItemAddOn.Where(x => x.OrderID == order.Id).ToList();
                var vm = new PaymentVM()
                {
                    Order = order,
                    MenuItem = lstSelectedItems.Select(x => x.MenuItem).FirstOrDefault(),
                    lstAddOns = lstSelectedItems.Select(x => x.AddOn).ToList()
                };
                var baseUrl = "https://localhost:44356/images/";
                vm.MenuItem.ImagePath = baseUrl + vm.MenuItem.ImagePath;
                totalCost += order.Price;
                lstVM.Add(vm);
            }
            
            string purchaseID = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
        .Substring(0, 22).Replace("/", "_").Replace("+", "-").TrimEnd('=');

            var checkOut = new CheckOutModel()
            {
                TotalAmount = totalCost.ToString(),
                ReceiptNumber = purchaseID
            };

            JsonObject createOrderRequest = new JsonObject();
            createOrderRequest.Add("intent", "CAPTURE");

            JsonObject amount = new JsonObject();
            amount.Add("currency_code", "USD");
            amount.Add("value", checkOut.TotalAmount.ToString());

            JsonObject purchaseUnit1 = new JsonObject();
            purchaseUnit1.Add("amount", amount);

            JsonArray purchaseUnits = new JsonArray();
            purchaseUnits.Add(purchaseUnit1);

            createOrderRequest.Add("purchase_units", purchaseUnits);

            string accessToken = await GetPayPalAccessToken();

            string url = $"{PayPalUrl}/v2/checkout/orders";

            string orderId;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var content = new StringContent(JsonSerializer.Serialize(createOrderRequest), null, "application/json");
                var result = await client.PostAsync(url, content);

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();

                    var jsonResponse = JsonNode.Parse(data);
                    if (jsonResponse != null)
                    {
                        orderId = jsonResponse["id"].ToString();

                        var response = new
                        {
                            id = orderId
                        };

                        return new JsonResult(response);
                    }
                }
                return new JsonResult(null);
            }

        }


        [HttpPost]
        public JsonResult CompleteOrder([FromBody] JsonObject data)
        {
            if (data == null)
            {
                return new JsonResult("");
            }

            string accessToken = GetPayPalAccessToken().Result;

            var url = $"{PayPalUrl}/v2/checkout/orders/{data["OrderID"].ToString()}/capture";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var content = new StringContent("", null, "application/json");
                var result = client.PostAsync(url, content).Result;

                if (result.IsSuccessStatusCode)
                {
                    var data2 = result.Content.ReadAsStringAsync().Result;

                    var jsonResponse = JsonNode.Parse(data2);
                    if (jsonResponse != null)
                    {
                        var status = jsonResponse["status"].ToString();
                        if (status == "COMPLETED")
                        {
                            return new JsonResult("success");

                        }
                    }
                }
                return new JsonResult(null);
            }
        }


        [HttpPost]
        public JsonResult CancelOrder([FromBody] JsonObject data)
        {
            if (data == null || data["orderID"] == null)
            {
                return new JsonResult("");
            }
            var orderID = data["orderID"].ToString()!;

            return new JsonResult("");
        }

        private async Task<string> GetPayPalAccessToken()
        {
            string accessToken;
            //var url = $"{PayPalUrl}/v1/oauth2/token" ;
            var url = $"https://api-m.sandbox.paypal.com/v1/oauth2/token";

            //var request = new HttpRequestMessage(HttpMethod.Post, "https://api-m.sandbox.paypal.com/v1/oauth2/token");

            using (var httpClient = new HttpClient())
            {
                var byteArray = new UTF8Encoding().GetBytes($"{PayPalClientId}:{PayPalSecret}");
                var request = new HttpRequestMessage(HttpMethod.Post, url);

                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
                var result = await httpClient.SendAsync(request);


                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();


                    var jsonResponse = JsonNode.Parse(readTask);
                    if (jsonResponse != null)
                    {
                        accessToken = jsonResponse["access_token"]?.ToString() ?? "";
                        return accessToken;
                    }
                }
                return accessToken = "";
            }
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            List<ShopCart> lstOrderIds = new List<ShopCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopCart>>(AppConst.Cart) != null
                && HttpContext.Session.Get<IEnumerable<ShopCart>>(AppConst.Cart).Count() > 0)
            {
                lstOrderIds = HttpContext.Session.Get<List<ShopCart>>(AppConst.Cart);
            }

            lstOrderIds.Remove(lstOrderIds.FirstOrDefault(u => u.OrderID == id));

            HttpContext.Session.Set(AppConst.Cart, lstOrderIds);
            return RedirectToAction("CheckOut");
            
        }

    }
}
