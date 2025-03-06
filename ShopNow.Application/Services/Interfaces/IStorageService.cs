using Microsoft.AspNetCore.Http;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IStorageService
	{
		Task<string> Upload(IFormFile file, string path);
	}
}
