using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Image { get; set; }

        public string? Description { get; set; }

        public string? Describe { get; set; }

        public decimal Price { get; set; }

      

        public int CategoryId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

        // Thuộc tính tính toán để lấy tổng tồn kho từ Inventory
        public int ComputedStockQuantity
        {
            get
            {
                return Inventories?.Sum(i => i.StockQuantity) ?? 0;
            }
        }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }


}
