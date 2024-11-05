    using BookStore.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly BookStoreContext _context;

        public OrderController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: /Cart/
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not authenticated
            }

            var oders = _context.Orders
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId)
                .ToList();

            return View(oders); // Pass the cart items to the view
        }

        // POST: /Cart/AddToCart
        [HttpPost] // Đảm bảo rằng phương thức này chỉ xử lý yêu cầu POST
        public IActionResult AddToCart(int id)
        {
            // Lấy sản phẩm từ cơ sở dữ liệu
            var product = _context.Products.Find(id);
            var userId = HttpContext.Session.GetString("UserId");

            if (product != null && userId != null)
            {
                var order = _context.Orders
                    .FirstOrDefault(ci => ci.ProductId == id && ci.UserId == userId);

                if (order == null)
                {
                    // Tạo một mục giỏ hàng mới
                    var newOrder = new Order
                    {
                        ProductId = id,
                        Quantity = 1,
                        OrderStatus = "Pending",
                        PaymentStatus = "Unpaid",
                        TotalAmount = product.Price,
                        OrderDate = DateTime.Now,
                        UserId = userId
                    };
                    _context.Orders.Add(newOrder);
                }
                else
                {
                    // Tăng số lượng sản phẩm nếu đã có trong giỏ hàng
                    order.Quantity++;
                    order.TotalAmount += product.Price;
                    _context.Orders.Update(order);
                }

                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }

            return RedirectToAction("Index", "Order"); // Chuyển hướng đến trang giỏ hàng
        }


        // POST: /Cart/Update
        [HttpPost]
        public IActionResult Update(int id, int quantity)
        {
            // Lấy order theo ID
            var order = _context.Orders.Include(o => o.Product).FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                TempData["Error"] = "Order không tồn tại.";
                return RedirectToAction("Index");
            }

            // Kiểm tra số lượng hợp lệ
            if (quantity <= 0)
            {
                TempData["Error"] = "Số lượng phải lớn hơn 0.";
                return RedirectToAction("Index");
            }

            // Cập nhật số lượng và tính toán lại tổng tiền
            order.Quantity = quantity;
            order.TotalAmount = order.Quantity * order.Product.Price; // Tính toán lại tổng tiền

            _context.Orders.Update(order);
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            TempData["Success"] = "Cập nhật thành công.";
            return RedirectToAction("Index");
        }



        // POST: /Cart/Remove
        [HttpPost]
        public IActionResult Remove(int id)
        {
            var order = _context.Orders.Find(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: /Cart/Checkout
        // GET: /Cart/Checkout'The key value at position 0 of the call to 'DbSet<User>.Find' was of type 'string', which does not match the property type of 'int'.'
        [HttpPost]
        public IActionResult Checkout()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Account");
            }

            // Attempt to parse userId as an int
            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest("Invalid user ID.");
            }

            // Retrieve user information for the checkout view
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound(); // Handle the case where the user is not found
            }

            // Retrieve orders associated with the user
            var orders = _context.Orders.Include(o => o.Product)
                                         .Where(o => o.UserId == userId.ToString()) // Ensure UserId is compared correctly
                                         .ToList();

            var checkoutViewModel = new CheckoutViewModel
            {
                User = user,
                Orders = orders
            };

            return View(checkoutViewModel); // Pass the CheckoutViewModel to the checkout view
        }



        [HttpPost]
        public IActionResult ProcessCheckout(List<int> productId)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Account");
            }

            // Xử lý đơn hàng cho các ID sản phẩm được cung cấp
            foreach (var id in productId)
            {
                // Lấy đơn hàng tương ứng với user và sản phẩm
                var order = _context.Orders.FirstOrDefault(o => o.ProductId == id && o.UserId == userIdString);
                if (order != null)
                {
                    // Chèn dữ liệu vào bảng OrderItem
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id // Giả định rằng OrderId là khóa ngoại trong OrderItem
                                           // Nếu cần thêm thuộc tính khác, hãy làm theo cách tương tự
                    };

                    _context.OrderItems.Add(orderItem); // Thêm OrderItem vào DbContext
                                                        // Cập nhật trạng thái của đơn hàng
                    order.OrderStatus = "Completed"; // Đánh dấu là đã hoàn thành
                    order.PaymentStatus = "Paid"; // Đánh dấu thanh toán đã hoàn thành
                    _context.Orders.Update(order); // Cập nhật đơn hàng
                }
            }

            _context.SaveChanges(); // Lưu tất cả thay đổi vào cơ sở dữ liệu
            TempData["Success"] = "Đơn hàng của bạn đã được xử lý thành công.";
            return RedirectToAction("Index", "Order"); // Chuyển hướng đến danh sách đơn hàng hoặc trang xác nhận
        }






    }


}


