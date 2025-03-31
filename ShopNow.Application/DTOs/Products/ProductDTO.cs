using ShopNow.Shared.Enums;

namespace ShopNow.Application.DTOs.Products
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }

        public ProductFeatured Featured { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public List<ProductVariantDTO>? Variants { get; set; }
    }
}
