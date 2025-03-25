using AutoMapper;
using ShopNow.Application.DTOs.Prodducts;
using ShopNow.Application.Services.Interfaces;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;

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
	}
}
