using ShopNow.Shared.Enums;

namespace ShopNow.Application.DTOs.Orders
{
	public class OrderDetailDTO
	{
		public Guid Id { get; set; }
		public OrderStatus OrderStatus { get; set; }
		public decimal ShipFee { get; set; }
		public decimal TotalCost { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public string ShippingAddress { get; set; } = null!;
		public List<OrderItemDTO> OrderItems { get; set; } = null!;
	}
}
