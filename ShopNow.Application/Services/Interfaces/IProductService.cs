using ShopNow.Application.DTOs.Products;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IProductService
	{
		Task<Guid> CreateProduct(CreateProductDTO createProductDTO);
		Task<ProductDetailDTO> GetProductDetail(Guid productId);

    }
}
