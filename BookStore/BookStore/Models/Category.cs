using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<CategoriesProduct> CategoriesProducts { get; set; } = new List<CategoriesProduct>();
}
