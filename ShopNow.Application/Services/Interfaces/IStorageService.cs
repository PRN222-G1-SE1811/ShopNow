using Microsoft.AspNetCore.Http;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IStorageService
	{
		/// <summary>
		/// Upload file then return file name
		/// </summary>
		/// <param name="file">Upload file</param>
		/// <param name="path"></param>
		/// <returns>Upload path</returns>
		Task<string> Upload(IFormFile file, string path);
	}
}
