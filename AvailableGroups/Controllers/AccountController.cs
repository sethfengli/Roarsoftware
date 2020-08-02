using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvailableGroups.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task Login(string returnUrl = "/")
        {
            var properties = new AuthenticationProperties() { RedirectUri = returnUrl };

            await HttpContext.ChallengeAsync("AuthRoar", properties);
        }

        // Need check with the API end, the function is kindly working, Logout works but
        // 1. The Oidc logout API call return params error, need check the special setting
        // 2. The page won't back to home page, it could be caused by problem 1
        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync("AuthRoar", new AuthenticationProperties
            {
                // **Allowed Logout URLs** settings for the client.
                RedirectUri = Url.Action("Index", "Home")
            });       
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
