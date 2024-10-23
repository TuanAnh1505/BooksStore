namespace ManageBook.Models
{
    public class Category
    {
        public int Id { get; set; }                  // ID thể loại
        public string Name { get; set; }             // Tên thể loại
        public ICollection<Book> Books { get; set; } // Liên kết nhiều sách
    }
}