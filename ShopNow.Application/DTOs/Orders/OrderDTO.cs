using ShopNow.Shared.Enums;

namespace ShopNow.Application.DTOs.Orders
{
	public class OrderDTO
	{
		public Guid Id { get; set; }
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
