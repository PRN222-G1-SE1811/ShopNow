using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class OrderItem : BaseEntity<Guid>, IHasCreatedAt, IHasDeletedAt
	{
		public required Guid OrderId { get; set; }
		public required Guid ProductId { get; set; }
		public required string Name { get; set; }
		public required int Quantity { get; set; }

		[Column(TypeName = "decimal(10,2)")]
		public required decimal Price { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public Order? Order { get; set; }

		[ForeignKey("ProductId")]
		public ProductVariant? Product { get; set; }
	}
}
