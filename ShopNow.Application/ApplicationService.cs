using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client.Extensions.Msal;
using ShopNow.Application.Services.Implements;
using ShopNow.Application.Services.Interfaces;

namespace ShopNow.Application
{
	public static class ApplicationService
	{
		public static void AddApplicationService(this IServiceCollection services)
		{
			services.AddScoped<IStorageService, StorageService>();
		}
	}
}
