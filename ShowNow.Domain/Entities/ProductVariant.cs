using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class ProductVariant : BaseEntity<Guid>, IHasUpdatedAt, IHasCreatedAt, IHasDeletedAt
	{
		public Guid ProductId { get; set; }
		public required string SKU { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
		public float? Discount { get; set; }
		public int Stock { get; set; }
		public int Sold { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public Product? Product { get; set; }
		public List<OrderItem>? OrderItems { get; set; }
		public List<ProductAsset>? ProductAssets { get; set; }
		public List<ProductAssetAttribute>? ProductAssetAttributes { get; set; }
		public List<Review>? Reviews { get; set; }
		public DateTime? DeletedAt { get; set; }
	}

}
