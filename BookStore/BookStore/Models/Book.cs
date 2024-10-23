using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace ManageBook.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }

}
