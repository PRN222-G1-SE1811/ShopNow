using ShopNow.Application.DTOs.Products;
using ShopNow.Presentation.Models.ProductViewModel;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IProductVariantService
	{
		Task<bool> CreateProdductVariants(Guid id, List<CreateProductVariantDTO> createProductVariantDTOs);
		Task DecreaseQuantity(Guid id, int quantity);
        Task<bool> EditProductVariantAsync(EditProductVariantDTO dto);
		Task<ProductVariant> GetProductVariantByIdAsync(Guid id);
        Task<List<AssetDTO>> GetAssetsByProductVariantIdAsync(Guid id); // Thêm phương thức này
        Task<bool> DeleteProductVariantAsync(Guid id);

    }
}
