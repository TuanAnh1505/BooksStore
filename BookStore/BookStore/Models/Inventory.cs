namespace ManageBook.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int StockQuantity { get; set; }
        public DateTime LastUpdate { get; set; }

        // Navigation properties
        public Product Product { get; set; }
    }

}
