using ShopNow.Shared.Enums;
using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class Order : BaseEntity<Guid>, IHasCreatedAt
	{
		[ForeignKey("Customer")]
		public required Guid CustomerId { get; set; }
		public required OrderStatus OrderStatus { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public required decimal ShipFee { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public required decimal TotalCost { get; set; }
		public required PaymentMethod PaymentMethod { get; set; }
		public Guid? CouponId { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? CancelledAt { get; set; }
		public DateTime? CompletedAt { get; set; }
		public DateTime? DeliveredAt { get; set; }
		public User? Customer { get; set; }
		public Coupon? Coupon { get; set; }
		public List<OrderItem>? OrderItems { get; set; }
	}
}
