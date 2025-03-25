using Microsoft.Extensions.DependencyInjection;
using ShopNow.Application.Services.Implements;
using ShopNow.Application.Services.Interfaces;

namespace ShopNow.Application
{
	public static class ApplicationService
	{
		public static void AddApplicationService(this IServiceCollection services)
		{
			services.AddScoped<IStorageService, StorageService>();
			services.AddAutoMapper(typeof(ApplicationService));

			#region add category service
			services.AddScoped<ICategoryService, CategoryService>();
			#endregion

			#region add product service
			services.AddScoped<IProductService, ProductService>();
			#endregion

			#region add product variant service
			services.AddScoped<IProductVariantService, ProductVariantService>();
			#endregion

			#region add SKU genrator
			services.AddScoped<ISKUGenerator, SKUGenerator>();
			#endregion

			#region add asset service
			services.AddScoped<IAssetService, AssetService>();
			#endregion

			#region add attribute service
			services.AddScoped<IAttributeService, AttributeService>();
			#endregion
		}
	}
}
