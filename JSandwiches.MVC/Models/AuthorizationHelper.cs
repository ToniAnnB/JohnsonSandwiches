using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace JSandwiches.MVC.Models
{
    public static class AuthorizationHelper
    {
        public static bool IsAuthenticated(HttpClient client, HttpContext httpContext, string baseUrl = "~/")
        {
            var returnUrl = httpContext.Request.GetDisplayUrl();
            string token = RetrieveTokenFromSession(httpContext);

            if (string.IsNullOrEmpty(token))
            {
                httpContext.Session.SetString("returnUrl", returnUrl ?? baseUrl);
                return false;
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }

        private static string RetrieveTokenFromSession(HttpContext httpContext)
        {
            string token = httpContext.Session.GetString("LoginSession") ?? string.Empty;
            return token;
        }
    }
}