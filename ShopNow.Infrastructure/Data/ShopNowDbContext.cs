using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopNow.Infrastructure.Configurations;
using ShowNow.Domain.Entities;
using Attribute = ShowNow.Domain.Entities.Attribute;

namespace ShopNow.Infrastructure.Data
{
	public class ShopNowDbContext : IdentityDbContext<
		User,
		IdentityRole<Guid>,
		Guid,
		IdentityUserClaim<Guid>,
		IdentityUserRole<Guid>,
		IdentityUserLogin<Guid>,
		IdentityRoleClaim<Guid>,
		IdentityUserToken<Guid>>
	{
		#region DbSet property
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Coupon> Coupons { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Asset> Assets { get; set; }
		public DbSet<Attribute> Attributes { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		#endregion

		public ShopNowDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ConfigureCategory();
			modelBuilder.ConfigureProduct();
			modelBuilder.ConfigureIdentity();
		}
	}
}
