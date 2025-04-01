using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopNow.Presentation.Models.ProductViewModel
{
    public class EditProductVariantDTO
    {
        [Required(ErrorMessage = "Product Variant ID is required.")]
        public required Guid Id { get; set; }

        [Required(ErrorMessage = "Discount is required.")]
        [Range(0, 100, ErrorMessage = "Discount must be between 0% and 100%.")]
        public required decimal Discount { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0.")]
        public required int Quantity { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Color must be between 3 and 30 characters.")]
        public required string Color { get; set; }

        [Required(ErrorMessage = "Size is required.")]
        [RegularExpression(@"^(S|M|L|XL|XXL|\d{2}|Free)$", ErrorMessage = "Invalid size format. Accepted values: S, M, L, XL, XXL, numbers (38, 39, ...), or 'Free'.")]
        public required string Size { get; set; }

        public List<IFormFile>? Assets { get; set; } = new List<IFormFile>();
    }
}
