using System.ComponentModel.DataAnnotations;
using BookStore.Models;

namespace BookStore.Dtos
{
    public class CreateProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [StringLength(500, ErrorMessage = "Describe cannot exceed 500 characters.")]
        public string? Describe { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
        public int ComputedStockQuantity { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
    }
}