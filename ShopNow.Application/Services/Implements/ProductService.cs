using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopNow.Application.DTOs.Prodducts;
using ShopNow.Application.Services.Interfaces;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;
using System.Linq.Expressions;

namespace ShopNow.Application.Services.Implements
{
	public class ProductService(IUnitOfWork<Product, Guid> unitOfWork, IMapper mapper) : IProductService
	{
		public async Task<Guid> CreateBaseProduct(CreateProductDTO createProductDTO)
		{
			var product = mapper.Map<Product>(createProductDTO);
			unitOfWork.GenericRepository.Insert(product);
			await unitOfWork.CommitAsync();
			return product.Id;
		}

		public async Task<ProductDetailDTO> GetProductDetail(Guid id)
		{
			var query = unitOfWork.GenericRepository.GetQueryAble();
			query = query.Include(_ => _.ProductVariants!).ThenInclude(_ => _.ProductAssets!).ThenInclude(_ => _.Asset)
				.Include(_ => _.ProductVariants!).ThenInclude(_ => _.ProductAssetAttributes!).ThenInclude(_ => _.Attribute);
			var product = await query.FirstOrDefaultAsync(_ => _.Id == id);
			return mapper.Map<ProductDetailDTO>(product);
		}
	}
}
