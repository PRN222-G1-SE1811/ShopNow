using Microsoft.AspNetCore.Http;
using ShopNow.Application.Services.Interfaces;

namespace ShopNow.Application.Services.Implements
{
	public class StorageService : IStorageService
	{
		public async Task<string> Upload(IFormFile file, string uploadPath)
		{
			if (file == null || file.Length == 0)
			{
				throw new ArgumentException("Invalid file");
			}

			if (!Directory.Exists(uploadPath))
			{
				Directory.CreateDirectory(uploadPath);
			}

			string filename = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
			string filePath = Path.Combine(uploadPath, filename);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			return $"/images/prodducts/{filename}";
		}

	}
}
