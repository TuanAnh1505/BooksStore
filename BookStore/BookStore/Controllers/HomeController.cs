using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using X.PagedList;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookStoreContext _context;

        public HomeController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var products = _context.Products.ToList();
            PagedList<Product> product = new PagedList<Product>(products, pageNumber, pageSize);
            return View(product);
        }


        //public IActionResult Details(int id)
        //{
        //    var product = _context.Products
        //        .Include(p => p.Category)
        //        .Include(p => p.Inventories)
        //        .SingleOrDefault(p => p.Id == id);
        //    if (product == null)
        //    {
        //        TempData["Message"] = $"Không th?y s?n ph?m có mã {id}";
        //        return Redirect("/404");
        //    }
        //    var result = new Product
        //    {
        //        Id = product.Id,
        //        Name = product.Name,

        //        Description = product.Description,
        //        Price = (int)product.Price,
        //        Image = product.Image,
        //        CategoryId = product.CategoryId,
        //    };
        //    return View(result);
        //}

        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventories)
                .SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }

            // Debugging information
            var inventoryCount = product.Inventories?.Count() ?? 0; // Get the count of inventories
            Console.WriteLine($"Inventory Count: {inventoryCount}"); // Log or break here to check

            return View(product);
        }


        public IActionResult NotFoundPage()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

    }
}
