using Microsoft.AspNetCore.Http;
using ShopNow.Application.Services.Interfaces;

namespace ShopNow.Application.Services.Implements
{
	public class StorageService : IStorageService
	{
		/// <summary>
		/// Upload file then return file name
		/// </summary>
		/// <param name="file">Upload file</param>
		/// <param name="uploadPath">Upload path</param>
		/// <returns>filename</returns>
		/// <exception cref="ArgumentNullException">Exception when file null</exception>
		public async Task<string> Upload(IFormFile file, string uploadPath)
		{
			if(file == null) throw new ArgumentNullException("Invalid file");
			if(!Directory.Exists(uploadPath))
			{
				Directory.CreateDirectory(uploadPath);
			}
			string filename = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
			string filePath = Path.Combine(uploadPath, filename);
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			return filename;
		}
	}
}
