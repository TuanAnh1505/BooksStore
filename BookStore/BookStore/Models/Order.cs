namespace ManageBook.Models
{
    public class Order
    {
        public int Id { get; set; }              // ID đơn hàng
        public DateTime OrderDate { get; set; }  // Ngày đặt hàng
        public decimal TotalAmount { get; set; } // Tổng số tiền
        public int CustomerId { get; set; }      // ID khách hàng
        public Customer Customer { get; set; }   // Liên kết đến khách hàng
        public ICollection<OrderDetail> OrderDetails { get; set; } // Chi tiết đơn hàng
    }

}