using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? ProductId { get; set; }

    public int? CustomerId { get; set; }

    public int? Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? ReviewDate { get; set; }

    public virtual User? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
