using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int? ProductId { get; set; }

    public int StockQuantity { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual Product? Product { get; set; }
}
