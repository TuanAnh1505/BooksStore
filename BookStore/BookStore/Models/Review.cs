namespace ManageBook.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }  // Điểm đánh giá từ 1-5
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        // Navigation properties
        public Product Product { get; set; }  // Liên kết đến bảng Products
        public User Customer { get; set; }  // Liên kết đến bảng Users
    }


}