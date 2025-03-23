using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class OrderItemAttributes : BaseEntity<Guid>
	{
		public Guid OrderItemId { get; set; }
		public Guid AttributeId { get; set; }

		[ForeignKey("OrderItemId")]
		public OrderItem OrderItem { get; set; }

		[ForeignKey("AttributeId")]
		public Attribute Attribute { get; set; }
	}
}
