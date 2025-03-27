using AutoMapper;
using ShopNow.Application.DTOs.Prodducts;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Shared.Enums;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;
using System;

namespace ShopNow.Application.Services.Implements
{
	public class ProductVariantService(IUnitOfWork<ProductVariant, Guid> unitOfWork, IMapper mapper, ISKUGenerator SKUGenerator, IAssetService assetService, IAttributeService attributeService) : IProductVariantService
	{
		public async Task CreateVariantProduct(ProductVariantDTO productVariantDTO)
		{
			var productVariant = mapper.Map<ProductVariant>(productVariantDTO.ProductDetail);
			productVariant.SKU = SKUGenerator.GenerateSKU();
			productVariant.CreatedAt = DateTime.Now;
			//productVariant.ProductAssets = new List<ProductAsset>() { productVariantDTO };
			unitOfWork.GenericRepository.Insert(productVariant);
			await unitOfWork.CommitAsync();
		}

		public async Task AddRangeVariantProduct(List<ProductVariantDTO> productVariantDTOs)
		{
			var productVariants = mapper.Map<List<ProductVariant>>(productVariantDTOs.Select(x => x.ProductDetail).ToList());

			for (int i = 0; i < productVariants.Count; i++)
			{
				// insert new asset
				var assetIds = await assetService.AddAssets(productVariantDTOs[i].ProductDetail.Files);

				// insert new attribute
				var attributeIds = await attributeService.AddAttributes(productVariantDTOs[i].AttributeDTOs);

				productVariants[i].SKU = SKUGenerator.GenerateSKU();
				productVariants[i].CreatedAt = DateTime.Now;
				productVariants[i].ProductAssets = new List<ProductAsset>();
				productVariants[i].ProductAssetAttributes = new List<ProductAssetAttribute>();

				for (var j = 0; j < assetIds.Count; j++)
				{
					// Gán asset vào danh sách
					productVariants[i].ProductAssets.Add(new ProductAsset
					{
						ProductId = productVariants[i].Id,
						AssetId = assetIds[j],
						Type = ProductAssetType.MainImage
					});

					// Gán attribute theo kiểu lặp lại (dùng modulo để không bị lỗi out-of-index)
					var attributeIndex = j % attributeIds.Count;
					productVariants[i].ProductAssetAttributes.Add(new ProductAssetAttribute
					{
						ProductId = productVariants[i].Id,
						AssetId = assetIds[j],
						AttributeId = attributeIds[attributeIndex], // Dùng modulo để gán attribute
						Value = "1920x1080"
					});
				}
			}

			await unitOfWork.GenericRepository.InsertRange(productVariants);
			await unitOfWork.CommitAsync();
		}

	}
}
