using ShopNow.Application.DTOs.Products;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IProductVariantService
	{
		Task<bool> CreateProdductVariants(Guid id, List<CreateProductVariantDTO> createProductVariantDTOs);
		Task DecreaseQuantity(Guid id, int quantity);
	}
}
