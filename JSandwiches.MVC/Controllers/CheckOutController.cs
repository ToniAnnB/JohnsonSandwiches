﻿using Azure.Core;
using JSandwiches.Models.Order;
using JSandwiches.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace JSandwiches.MVC.Controllers
{
    [IgnoreAntiforgeryToken]
    public class CheckOutController : Controller
    {
        public string PayPalClientId { get; set; } = null;
        private string PayPalSecret { get; set; } = null;
        public string PayPalUrl { get; set; } = null;


        public CheckOutController(IConfiguration configuration)
        {
            PayPalClientId = configuration["PayPalSettings:ClientId"]!;
            PayPalSecret = configuration["PayPalSettings:Secret"]!;
            PayPalUrl = configuration["PayPalSettings:Url"]!;
        }
        public IActionResult CheckOut()
        {
            ViewBag.PayPalClientId = PayPalClientId;
            ViewBag.PayPalSecret = PayPalSecret;
            ViewBag.PayPalUrl = PayPalUrl;

            var checkOut = new CheckOutModel()
            {
                TotalAmount = "90.00",
                ReceiptNumber = "0382073091ydg2893798y"
            };
            return View(checkOut);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrder()
        {
            var checkOut = new CheckOutModel()
            {
                TotalAmount = "390.00",
                ReceiptNumber = "0382073091ydg2893798y"
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
                        if(status == "COMPLETED")
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

    }
}