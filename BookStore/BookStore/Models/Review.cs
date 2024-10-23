namespace ManageBook.Models
{
    public class Review
    {
        public int Id { get; set; }           // ID đánh giá
        public int Rating { get; set; }       // Điểm đánh giá (1-5)
        public string Comment { get; set; }   // Nhận xét
        public int BookId { get; set; }       // Liên kết đến sách
        public Book Book { get; set; }        // Đối tượng sách
        public int CustomerId { get; set; }   // Liên kết đến khách hàng
        public Customer Customer { get; set; }// Đối tượng khách hàng
    }

}