using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginConfirm(string email ,string password)
        {
            if(email=="m@gmail.com" && password=="123456")
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Login(");
        }


        ////
        //[HttpPost]
        //public async Task<IActionResult> Login(string email, string password)
        //{ 
        //  if(email =="admon@test.com" && password =="123456")
        //  {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, email),
        //            new Claim(ClaimTypes.NameIdentifier,"1")
        //        };
        //        var identity = new ClaimsIdentity(claims, "login");
        //        var principal = new ClaimsPrincipal(identity);
        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
        //        return RedirectToAction("Index", "Home");
        //  }
        //    ViewBag.ErrorMessage = "Invalid email or password";
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction("Login");
        //}
        
    }
}
