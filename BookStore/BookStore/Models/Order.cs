namespace ManageBook.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string OrderStatus { get; set; }   // 'Pending', 'Completed', 'Cancelled'
        public string PaymentStatus { get; set; } // 'Pending', 'Paid', 'Failed'
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        // Navigation properties
        public User Customer { get; set; }  // Liên kết đến bảng Users
        public ICollection<OrderItem> OrderItems { get; set; }
    }


}