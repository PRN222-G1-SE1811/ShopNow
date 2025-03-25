using ShopNow.Application.DTOs.Prodducts;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IProductService
	{
		Task<Guid> CreateBaseProduct(CreateProductDTO createProductDTO);
	}
}
