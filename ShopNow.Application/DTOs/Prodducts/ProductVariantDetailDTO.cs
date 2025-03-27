namespace ShopNow.Application.DTOs.Prodducts
{
	public class ProductVariantDetailDTO
	{
		public Guid Id { get; set; }
		public string SKU { get; set; } = null!;
		public decimal Price { get; set; }
		public float Discount { get; set; }
		public int Stock { get; set; }
		public int Sold { get; set; }
		public IEnumerable<string> Images { get; set; } = null!;
	}
}
