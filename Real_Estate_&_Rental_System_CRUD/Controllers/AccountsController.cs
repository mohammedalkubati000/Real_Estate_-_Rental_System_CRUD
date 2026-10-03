using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_Estate___Rental_System_CRUD.Data;
using System.Security.Claims;
namespace Real_Estate___Rental_System_CRUD.Controllers
{
    //[Authorize]
    public class AccountsController : Controller
    {
        
        private readonly AppDbContext _db;
        public AccountsController(AppDbContext db)
        {
            _db = db;
        }

        //////

        //Login

        //////
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginConfirm(string email ,string password)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == email);
            if (User == null)
            {
                return NotFound();
            }

                    bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, user.Password);


            if (!isPasswordCorrect)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View("Login");
            }
            //if (user.IsLocked)
            //{
            //    ModelState.AddModelError("", "Your account is locked");
            //    return View("Login");
            //}





            
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                var identity = new ClaimsIdentity(claims, "login");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            
           
        }


        //////

        //Logout

        //////
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
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
