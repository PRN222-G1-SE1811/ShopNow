using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShowNow.Domain.Entities;
using System.Reflection.Emit;

namespace ShopNow.Infrastructure.Configurations
{
	public static class IdentityConfiguration
	{
		public static void ConfigureIdentity(this ModelBuilder modelBuilder)
		{
			// Configure composite keys first
			modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
			modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(r => new { r.UserId, r.RoleId });
			modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

			// Then rename the tables
			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
			modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
			modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
			modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
			modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
			modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
		}
	}
}
