using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Users")]
    public class UsersController : Controller
    {
        private readonly BookStoreContext _context;

        public UsersController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Users
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
