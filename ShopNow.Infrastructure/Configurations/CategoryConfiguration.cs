using Microsoft.EntityFrameworkCore;
using ShowNow.Domain.Entities;

namespace ShopNow.Infrastructure.Configurations
{
	public static class CategoryConfiguration
	{
		public static void ConfigureCategory(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>(x =>
			{
				x.HasIndex(c => c.Slug);
			});
		}
	}
}
