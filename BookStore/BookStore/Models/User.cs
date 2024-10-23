namespace ManageBook.Models
{
    public class User
    {
        public int Id { get; set; }              // ID người dùng
        public string Name { get; set; }         // Tên người dùng
        public string Email { get; set; }        // Email (dùng để đăng nhập)
        public string Password { get; set; }     // Mật khẩu (cần mã hóa)
        public string Role { get; set; }         // Vai trò (Customer, Employee)
        public bool IsActive { get; set; }       // Trạng thái hoạt động của tài khoản
    }
}