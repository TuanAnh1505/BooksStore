namespace ManageBook.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public string Position { get; set; }  // 'Manager', 'Accountant', 'WarehouseKeeper'
        public DateTime HireDate { get; set; }

        // Navigation properties
        public User User { get; set; }  // Liên kết đến bảng Users
    }


}
