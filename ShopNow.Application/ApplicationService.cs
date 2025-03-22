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
		}
	}
}
