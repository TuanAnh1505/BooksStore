using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Product> Products { get; set; }
}
