namespace ShopNow.Application.DTOs.Orders
{
	public class OrderItemDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
