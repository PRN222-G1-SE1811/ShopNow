namespace ShopNow.Application.DTOs.Carts
{
	public class CartItemDTO
	{
		public Guid CartId { get; set; }
		public string ProductName { get; set; } = null!;
		public string ProductImage { get; set; } = null!;
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
