using AutoMapper;
using ShopNow.Application.DTOs.Products;
using ShopNow.Application.Services.Interfaces;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;

namespace ShopNow.Application.Services.Implements
{
	public class ProductVariantService(IUnitOfWork<ProductVariant, Guid> unitOfWork, IAssetService assetService, ISKUGenerator SKUGenerator, IMapper mapper) : IProductVariantService
	{
		public async Task<bool> CreateProdductVariants(Guid id, List<CreateProductVariantDTO> createProductVariantDTOs)
		{
			var productVariantDomain = mapper.Map<List<ProductVariant>>(createProductVariantDTOs);
			foreach (var p in productVariantDomain)
			{
				p.ProductId = id;
				p.Sold = 0;
				p.SKU = SKUGenerator.GenerateSKU();
				unitOfWork.GenericRepository.Insert(p);
			}
			
			var rowChanged = await unitOfWork.CommitAsync();
			var i = 0;
			foreach (var productVariant in createProductVariantDTOs)
			{
				await assetService.CreateAssetsRange(productVariantDomain[i++].Id, productVariant.Assets);
			}
			i = 0;
			return rowChanged > 0;
		}
	}
}
