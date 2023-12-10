using JSandwiches.Models.DTO.UsersDTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

namespace JSandwiches.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _client;

        const string Session_Auth = "LoginSession";
        const string Session_Role = "RoleSession";

        public LoginController(IHttpClientFactory factoryClient)
        {
            _client = factoryClient.CreateClient("AuthAPI");
        }


        [HttpGet]
        [Route("/Login")]
        public IActionResult Login()
        {
            var appUser = new LoginAppUserDTO();
            return View(appUser);
        }


        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(LoginAppUserDTO appUser)
        {
            var sAppUser = JsonConvert.SerializeObject(appUser);
            var content = new StringContent(sAppUser, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}/login", content);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);

                if (responseData.ContainsKey("status") && responseData["status"].ToString() == "success")
                {
                    if (responseData.ContainsKey("data") && responseData["data"] is JObject jObject)
                    {
                        var token = JsonConvert.SerializeObject(responseData["data"]);

                        HttpContext.Session.SetString(Session_Auth, token);

                        var lstRoles = responseData["roles"].ToString();

                        if (lstRoles.Contains("Admin"))
                        {
                            HttpContext.Session.SetString(Session_Role, "Admin");
                        }
                        else if (lstRoles.Contains("CService"))
                        {
                            HttpContext.Session.SetString(Session_Role, "CService");
                        }
                        else if (lstRoles.Contains("Kitchen"))
                        {
                            HttpContext.Session.SetString(Session_Role, "Kitchen");
                        }
                        else
                        {
                            HttpContext.Session.SetString(Session_Role, "Customer");
                        }




                        var returnUrl = HttpContext.Session.GetString("returnUrl")!;
                        if (returnUrl == null)
                        {
                            return RedirectToAction("Dashboard", "Home");

                        }
                        return Redirect(returnUrl);
                    }
                }

            }
            ViewBag.LoginError = "Incorrect credentials";
            return View(appUser);

        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove(Session_Auth);
            return RedirectToAction("Index", "Home");
        }
    }
}
