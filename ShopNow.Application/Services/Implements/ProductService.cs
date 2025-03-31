using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopNow.Application.DTOs.Products;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Infrastructure.Data;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;
using System.Collections.Generic;

namespace ShopNow.Application.Services.Implements
{
	public class ProductService(IUnitOfWork<Product, Guid> unitOfWork, IMapper mapper) : IProductService
	{
       
        public async Task<Guid> CreateProduct(CreateProductDTO createProductDTO)
		{
			var productDomain = mapper.Map<Product>(createProductDTO);
			var createdProduct = unitOfWork.GenericRepository.Insert(productDomain);
			await unitOfWork.CommitAsync();
			return createdProduct.Id;
		}

		public async Task<ProductDetailDTO> GetProductDetail(Guid productId)
		{
			var query = unitOfWork.GenericRepository.GetQueryAble();
			query = query.Include(_ => _.Category)
				.Include(_ => _.ProductVariants!).ThenInclude(_ => _.Assets);
			var productDetails = await query.FirstOrDefaultAsync(_ => _.Id == productId && _.Status == Shared.Enums.ProductStatus.Active);
			return mapper.Map<ProductDetailDTO>(productDetails);
		}
    }
}
