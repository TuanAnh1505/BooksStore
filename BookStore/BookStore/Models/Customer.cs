namespace ManageBook.Models
{
    public class Customer : User
    {
        public int Id { get; set; }                    // ID khách hàng
        public string Name { get; set; }               // Tên khách hàng
        public string Email { get; set; }              // Email
        public ICollection<Review> Reviews { get; set; } // Danh sách các đánh giá
        public ICollection<Order> Orders { get; set; }    // Các đơn hàng của khách hàng
    }

}