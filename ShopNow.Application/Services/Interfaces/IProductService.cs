using ShopNow.Application.DTOs.Products;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IProductService
	{
		Task<Guid> CreateProduct(CreateProductDTO createProductDTO);
		Task<ProductDetailDTO> GetProductDetail(Guid productId);
	}
}
