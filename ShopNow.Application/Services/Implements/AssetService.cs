using Microsoft.AspNetCore.Http;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Shared.Enums;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;

namespace ShopNow.Application.Services.Implements
{
	public class AssetService(IUnitOfWork<Asset, Guid> unitOfWork, IStorageService storageService) : IAssetService
	{
		public async Task<int> CreateAssetsRange(Guid productVariantId, List<IFormFile> files)
		{
			var assets = new List<Asset>();
			foreach (var file in files)
			{
				assets.Add(new Asset()
				{
					FileName = file.FileName,
					Path = await storageService.Upload(file, "wwwroot\\images\\products"),
					Type = System.IO.Path.GetExtension(file.FileName) switch
					{
						".jpg" or ".jpeg" or ".png" => AssetType.Image,
						".pdf" => AssetType.Document,
						".mp4" => AssetType.Video,
					},
					Size = file.Length,
					CreatedAt = DateTime.Now,
					ProductVariantId = productVariantId
				});
			}
			await unitOfWork.GenericRepository.InsertRange(assets);
			return await unitOfWork.CommitAsync();
		}
	}
}
