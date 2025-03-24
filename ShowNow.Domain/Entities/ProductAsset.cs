using ShopNow.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	public class ProductAsset
	{
		public Guid ProductId { get; set; }
		public Guid AssetId { get; set; }
		public required ProductAssetType Type { get; set; }

		[ForeignKey("ProductId")]
		public ProductVariant? Product { get; set; }
		public Asset? Asset { get; set; }
	}
}
