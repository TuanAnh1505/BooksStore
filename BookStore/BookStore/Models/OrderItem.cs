namespace ManageBook.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation properties
        public Order Order { get; set; }  // Liên kết đến bảng Orders
        public Product Product { get; set; } // Liên kết đến bảng Products
    }

}