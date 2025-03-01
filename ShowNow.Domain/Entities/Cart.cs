using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class Cart : BaseEntity<Guid>, IHasCreatedAt, IHasUpdatedAt
	{
		[ForeignKey("Customer")]
		public Guid CustomerId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public User? Customer { get; set; }
		public List<CartItem>? CartItems { get; set; }
	}
}
