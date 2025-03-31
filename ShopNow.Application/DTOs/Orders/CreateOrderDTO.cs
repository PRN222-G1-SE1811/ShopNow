using ShopNow.Shared.Enums;

namespace ShopNow.Application.DTOs.Orders
{
	public class CreateOrderDTO
	{
		public Guid CustomerId { get; set; }
		public OrderStatus OrderStatus { get; set; }
		public decimal ShipFee { get; set; }
		public decimal TotalCost { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public Guid CouponId { get; set; }
		public DateTime CreatedAt { get; set; }
		public string ShippingAddress { get; set; } = null!;
	}
}
