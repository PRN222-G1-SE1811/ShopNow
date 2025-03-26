using Microsoft.EntityFrameworkCore;
using ShowNow.Domain.Entities;

namespace ShopNow.Infrastructure.Configurations
{
	public static class ProductConfiguration
	{
		public static void ConfigureProduct(this ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<ProductAsset>(x =>
			{
				x.HasKey(pa => new { pa.ProductId, pa.AssetId });
			});

			modelBuilder.Entity<ProductAssetAttribute>(x =>
			{
				x.HasKey(pab => new { pab.ProductId, pab.AssetId, pab.AttributeId });
			});
		}
	}
}
