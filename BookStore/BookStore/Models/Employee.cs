using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int? UserId { get; set; }

    public string Position { get; set; } = null!;

    public DateTime? HireDate { get; set; }

    public virtual User? User { get; set; }
}
