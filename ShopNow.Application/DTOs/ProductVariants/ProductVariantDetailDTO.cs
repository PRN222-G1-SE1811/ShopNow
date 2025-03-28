namespace ShopNow.Application.DTOs.ProductVariants
{
	public class ProductVariantDetailDTO
	{
		public Guid Id { get; set; }
		public string SKU { get; set; } = null!;
		public float Discount { get; set; }
		public int Quantity { get; set; }
		public int Sold { get; set; }
		public string Color { get; set; } = null!;
		public string Size { get; set; } = null!;
		public IEnumerable<string> Images { get; set; } = null!;
	}
}
