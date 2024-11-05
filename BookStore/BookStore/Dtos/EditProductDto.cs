using System.ComponentModel.DataAnnotations;

namespace BookStore.Dtos
{
    public class EditProductDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string? Describe { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int ComputedStockQuantity { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
