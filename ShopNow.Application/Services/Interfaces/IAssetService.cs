using Microsoft.AspNetCore.Http;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IAssetService
	{
		Task<int> CreateAssetsRange(Guid productVariantId, List<IFormFile> files);
	}
}
