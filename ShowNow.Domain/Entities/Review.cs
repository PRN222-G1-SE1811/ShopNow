using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class Review :BaseEntity<Guid>, IHasCreatedAt, IHasUpdatedAt
	{
		public required Guid ProductId { get; set; }

		[ForeignKey("Customer")]
		public required Guid CustomerId { get; set; }
		public required int Rating { get; set; }

		[Column(TypeName = "varchar(255)")]
		public string? Title { get; set; }

		[Column(TypeName = "text")]
		public string? Content { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public Product? Product { get; set; }
		public User? Customer { get; set; }
	}
}
