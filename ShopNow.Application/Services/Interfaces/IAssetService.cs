using Microsoft.AspNetCore.Http;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IAssetService
	{
		Task<List<Guid>> AddAssets(List<IFormFile> Files);
	}
}
