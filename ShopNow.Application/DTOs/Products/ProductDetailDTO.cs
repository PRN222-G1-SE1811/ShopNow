using ShopNow.Application.DTOs.ProductVariants;

namespace ShopNow.Application.DTOs.Products
{
	public class ProductDetailDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public decimal Price { get; set; }
		public string Category { get; set; } = null!;
		public IEnumerable<ProductVariantDetailDTO> ProductVariants { get; set; } = null!;
	}
}
