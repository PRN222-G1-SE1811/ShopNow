using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class CartItem : BaseEntity<Guid>, IHasCreatedAt, IHasUpdatedAt
	{
		public Guid CartId { get; set; }
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal Price { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public Product? Product { get; set; }
		public Cart? Cart { get; set; }
	}
}
