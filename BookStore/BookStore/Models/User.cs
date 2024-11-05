using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public partial class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(256)]
    public string PasswordHash { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(15)] 
    public string Phone { get; set; } = null!;

    [StringLength(256)]
    public string Address { get; set; } = null!;

    public string Role { get; set; } = "User";

    public DateTime? CreatedAt { get; set; }


    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
