using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_Estate___Rental_System_CRUD.Repositories;
using Real_Estate___Rental_System_CRUD.Repositories.Base;
using System.Security.Claims;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    //[Authorize]
    public class AccountsController : Controller
    {
        // === Old DbContext Reference (Commented Out) ===
        // private readonly AppDbContext _db;
        // public AccountsController(AppDbContext db)
        // {
        //     _db = db;
        // }

        // === Old Repository Pattern Injection (Commented Out) ===
        // private readonly IUserRepository _userRepository;
        // public AccountsController(IUserRepository userRepository)
        // {
        //     _userRepository = userRepository;
        // }

        // === New UnitOfWork Injection ===
        private readonly IUnitOfWork _unitOfWork;

        public AccountsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //////

        //Login

        //////
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginConfirm(string email, string password)
        {
            // === Old DbContext Code (Commented Out) ===
            // var user = _db.Users.FirstOrDefault(x => x.Email == email);

            // === Old Repository Code (Commented Out) ===
            // var user = await _userRepository.GetUserByEmailAsync(email);

            // === New Repository Code via UnitOfWork ===
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);

            if (user == null)
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