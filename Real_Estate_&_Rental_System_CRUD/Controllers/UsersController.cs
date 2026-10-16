using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_Estate___Rental_System_CRUD.Models;
using Real_Estate___Rental_System_CRUD.Repositories;
using Real_Estate___Rental_System_CRUD.Repositories.Base;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // === Old DbContext Reference (Commented Out) ===
        // private readonly AppDbContext _db;
        // public UsersController(AppDbContext db)
        // {
        //     _db = db;
        // }

        // === Old Repository Pattern Injection (Commented Out) ===
        // private readonly IUserRepository _userRepository;

        // public UsersController(IUserRepository userRepository)
        // {
        //     _userRepository = userRepository;
        // }

        // === New UnitOfWork Injection ===
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // Index
        // =========================
        public async Task<IActionResult> Index()
        {
            // === Old DbContext Code (Commented Out) ===
            // var users = _db.Users.ToList();
            // return View(users);

            // === Old Repository Code (Commented Out) ===
            // var users = await _userRepository.GetAllUsersAsync();

            // === New Repository Code via UnitOfWork ===
            var users = await _unitOfWork.UserRepository.GetAllUsersAsync();
            return View(users);
        }

        // =========================
        // Details
        // =========================
        public async Task<IActionResult> Details(int id)
        {
            // === Old DbContext Code (Commented Out) ===
            // var user = _db.Users.Find(id);

            // === New Repository Code via UnitOfWork ===
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // =========================
        // Create GET
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // Create POST
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // === Old DbContext Code (Commented Out) ===
                // _db.Users.Add(user);
                // _db.SaveChanges();

                // === New Repository Code via UnitOfWork ===
                await _unitOfWork.UserRepository.AddUserAsync(user);

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // =========================
        // Edit GET
        // =========================
        public async Task<IActionResult> Edit(int id)
        {
            // === Old DbContext Code (Commented Out) ===
            // var user = _db.Users.Find(id);

            // === New Repository Code via UnitOfWork ===
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            ModelState.Remove("Password"); // رفض العملية إذا كان فارغاً
            if (ModelState.IsValid)
            {
                // === Old DbContext Code (Commented Out) ===
                // var oldUser = _db.Users.Find(user.Id);
                // if (oldUser == null)
                //     return NotFound();
                // oldUser.Name = user.Name;
                // oldUser.UserName = user.UserName;
                // oldUser.Email = user.Email;
                // oldUser.IsLocked = user.IsLocked;
                // if (!string.IsNullOrEmpty(user.Password))
                // {
                //     oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                // }
                // _db.SaveChanges();

                // === New Repository Code via UnitOfWork ===
                var oldUser = await _unitOfWork.UserRepository.GetUserByIdAsync(user.Id);

                if (oldUser == null)
                    return NotFound();

                oldUser.Name = user.Name;
                oldUser.UserName = user.UserName;
                oldUser.Email = user.Email;
                oldUser.IsLocked = user.IsLocked;

                if (!string.IsNullOrEmpty(user.Password))
                {
                    oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }

                await _unitOfWork.UserRepository.UpdateUserAsync(oldUser);

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // =========================
        // Delete GET
        // =========================
        public async Task<IActionResult> Delete(int id)
        {
            // === Old DbContext Code (Commented Out) ===
            // var user = _db.Users.Find(id);

            // === New Repository Code via UnitOfWork ===
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            // === Old DbContext Code (Commented Out) ===
            // var user = _db.Users.Find(id);
            // if (user == null)
            //     return NotFound();
            // _db.Users.Remove(user);
            // _db.SaveChanges();

            // === New Repository Code via UnitOfWork ===
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            await _unitOfWork.UserRepository.DeleteUserAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}