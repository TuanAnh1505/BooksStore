using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly BookStoreContext _context;

        public AccountController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu username đã tồn tại
                if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Username is already taken.");
                    return View(user);
                }

                // Gán role cho user
                user.Role = user.Username == "admin" ? "Admin" : "User"; // Gán role Admin cho tài khoản admin

                // Hash password
                user.PasswordHash = HashPassword(user.PasswordHash);
                user.CreatedAt = DateTime.Now;

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }

            return View(user);
        }


        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role); // Lưu role vào session

                // Điều hướng theo vai trò
                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" }); // Redirect đến Admin
                }
                else
                {
                    return RedirectToAction("Index", "Home"); // Redirect đến Home cho User
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }


        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Helper methods to hash and verify password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
