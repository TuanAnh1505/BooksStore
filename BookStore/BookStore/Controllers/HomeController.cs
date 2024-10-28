using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookStoreContext _context;

        public HomeController(BookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                TempData["Message"] = $"Không th?y s?n ph?m có mã {id}";
                return Redirect("/404");
            }
            var result = new Product
            {
                Id = product.Id,
                Name = product.Name,
                StockQuantity = product.StockQuantity,
                Description = product.Description,
                Price = (int)product.Price,
                Image = product.Image,
                CategoryId = product.CategoryId,
            };
            return View(result);
        }





        // Add to Cart functionality
        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null) return RedirectToAction("Index");

            // Retrieve existing cart from session or create a new one
            var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();

            // Check if product already exists in cart
            var existingProduct = cart.FirstOrDefault(p => p.Id == id);
            if (existingProduct != null)
            {
                existingProduct.StockQuantity++; // Increment quantity if already in cart
            }
            else
            {
                // Clone the product to avoid modifying the original product
                var productToAdd = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Image = product.Image,
                    Description = product.Description,
                    StockQuantity = 1 // Add product with quantity 1
                };
                cart.Add(productToAdd);
            }

            // Save updated cart back to session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Carts");
        }

        public IActionResult Carts()
        {
            // Retrieve cart from session
            var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult UpdateCart(Dictionary<int, int> quantities)
        {
            // Retrieve cart from session
            var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();

            foreach (var item in cart)
            {
                if (quantities.ContainsKey(item.Id))
                {
                    item.StockQuantity = quantities[item.Id]; // Update quantity based on user input
                }
            }

            // Save updated cart back to session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Carts");
        }


        public IActionResult NotFound()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }
    }
}
