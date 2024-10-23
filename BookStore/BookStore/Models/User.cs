namespace ManageBook.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }  // Role có thể là 'Owner', 'Staff', 'Customer', 'Manager', 'Accountant', 'WarehouseKeeper'
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }

}