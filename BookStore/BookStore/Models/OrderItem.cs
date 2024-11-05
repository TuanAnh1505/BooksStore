using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }

}
