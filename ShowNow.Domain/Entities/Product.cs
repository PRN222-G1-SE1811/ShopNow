using ShopNow.Shared.Enums;
using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class Product : BaseEntity<Guid>, IHasUpdatedAt, IHasCreatedAt, IHasDeletedAt
	{
		public Guid CategoryID { get; set; }

		[Column(TypeName = "varchar(255)")]
		public required string Name { get; set; }

		[Column(TypeName = "varchar(255)")]
		public required string Slug { get; set; }

		[Column(TypeName = "text")]
		public string? Description { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public required decimal Price { get; set; }
		public float? Discount { get; set; }
		public required int Quantity { get; set; }
		public int? Sold { get; set; }
		public required ProductStatus Status { get; set; }
		public required ProductFeatured Featured { get; set; }
		public DateTime UpdatedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public Category? Category { get; set; }
		public List<Review>? Reviews { get; set; }
		public List<ProductAsset>? ProductAssets { get; set; }
		public List<ProductAssetAttribute>? ProductAssetAttributes { get; set; }
	}
}
