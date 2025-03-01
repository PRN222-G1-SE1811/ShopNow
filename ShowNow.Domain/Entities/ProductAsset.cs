using ShopNow.Shared.Enums;

namespace ShowNow.Domain.Entities
{
	public class ProductAsset
	{
		public Guid ProductId { get; set; }
		public Guid AssetId { get; set; }
		public required ProductAssetType Type { get; set; }
		public Product? Product { get; set; }
		public Asset? Asset { get; set; }
	}
}
