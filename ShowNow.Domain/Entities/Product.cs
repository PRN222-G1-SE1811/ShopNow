using ShopNow.Shared.Enums;
using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class Product : BaseEntity<Guid>, IHasUpdatedAt, IHasCreatedAt, IHasDeletedAt
	{
		public Guid CategoryID { get; set; }


		[Column(TypeName = "varchar(255)")]
		public required string Name { get; set; }

		[Column(TypeName = "text")]
		public string? Description { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
		public required ProductStatus Status { get; set; }
		public required ProductFeatured Featured { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public Category? Category { get; set; }
		public List<ProductVariant>? ProductVariants { get; set; }
	}
}
