using Microsoft.Extensions.DependencyInjection;
using ShopNow.Infrastructure.Repositories;
using ShowNow.Domain.Interfaces;

namespace ShopNow.Infrastructure
{
	public static class InfrastructureService
	{
		public static void AddInfrastructureService(this IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
			services.AddScoped(typeof(IUnitOfWork<,>), typeof(UnitOfWork<,>));
		}
	}
}
