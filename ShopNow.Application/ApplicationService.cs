using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopNow.Application.DTOs.Mail;
using ShopNow.Application.Services.Implements;
using ShopNow.Application.Services.Interfaces;

namespace ShopNow.Application
{
	public static class ApplicationService
	{
		public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
		{
			#region add storage service
			services.AddScoped<IStorageService, StorageService>();
			#endregion

			#region add automapper
			services.AddAutoMapper(typeof(ApplicationService));
			#endregion

			#region add category service
			services.AddScoped<ICategoryService, CategoryService>();
			#endregion

			#region send mail
			//Dang ki mail
			services.AddOptions();
			var mailsetting = configuration.GetSection("MailSettings");
			services.Configure<MailSettings>(mailsetting);
			services.AddSingleton<IEmailSender, SendMailService>();
			#endregion

			#region send mail
			//IdentityErrorServices
			services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();
			#endregion

			#region add product service
			services.AddScoped<IProductService, ProductService>();
			#endregion

			#region add product variant service
			services.AddScoped<IProductVariantService, ProductVariantService>();
			#endregion

			#region add asset service
			services.AddScoped<IAssetService, AssetService>();
			#endregion

			#region add SKU service
			services.AddScoped<ISKUGenerator, SKUGenerator>();
			#endregion

			#region add cart service
			services.AddScoped<ICartService, CartService>();
			#endregion

			#region add user user service
			services.AddScoped<IUserService, UserService>();
			#endregion

			#region add shipping service
			services.AddScoped<IShippingService, ShippingService>();
			#endregion

			#region add order service
			services.AddScoped<IOrderService, OrderService>();
			#endregion
		}
	}
}
