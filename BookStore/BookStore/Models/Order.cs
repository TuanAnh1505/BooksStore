using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Order
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime? OrderDate { get; set; }

    public string UserId { get; set; }

    public Product Product { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
