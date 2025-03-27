using ShopNow.Shared.Enums;

namespace ShopNow.Application.DTOs.Prodducts
{
	public class ProductDetailDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public IEnumerable<ProductVariantDetailDTO> ProductVariants { get; set; } = null!;
	}
}
