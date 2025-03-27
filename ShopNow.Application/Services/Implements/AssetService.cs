using Microsoft.AspNetCore.Http;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Shared.Enums;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;

namespace ShopNow.Application.Services.Implements
{
	public class AssetService(IUnitOfWork<Asset, Guid> unitOfWork, IStorageService storageService) : IAssetService
	{
		public async Task<List<Guid>> AddAssets(List<IFormFile> Files)
		{
			var assets = new List<Asset>();
			foreach (var file in Files)
			{
				assets.Add(new Asset()
				{
					FileName = file.FileName,
					Path = await storageService.Upload(file, "wwwroot\\images"),
					Type = System.IO.Path.GetExtension(file.FileName) switch
					{
						".jpg" or ".jpeg" or ".png" => AssetType.Image,
						".pdf" => AssetType.Document,
						".mp4" => AssetType.Video,
					},
					Size = file.Length,
					CreatedAt = DateTime.Now,
				});
			}
			await unitOfWork.GenericRepository.InsertRange(assets);
			return assets.Select(_ => _.Id).ToList();
		}
	}
}
