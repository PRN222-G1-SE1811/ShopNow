using ShopNow.Application.DTOs.Categories;
using ShopNow.Application.DTOs.Products;
using ShopNow.Presentation.Models.ProductViewModel;
using ShopNow.Shared.Paginators;
using ShowNow.Domain.Entities;

namespace ShopNow.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<Guid> CreateProduct(CreateProductDTO createProductDTO);
        Task<bool> EditProduct(EditProductDTO editeProductDTO);
        Task<EditProductDTO> GetProductEditById(Guid id);
        Task<ProductDetailDTO> GetProductDetail(Guid productId);


        public Task<bool> DeleteProduct(Guid id);


        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<(List<ProductDTO> Products, int TotalCount)> GetPaginatedAsync(
     int pageIndex,
     int pageSize,
     string? search = null,
     Guid? categoryId = null, // Thêm categoryId để lọc theo danh mục
     decimal? minPrice = null,
     decimal? maxPrice = null,
     string? sortBy = null,
     string? sortOrder = "asc");

    }
}
