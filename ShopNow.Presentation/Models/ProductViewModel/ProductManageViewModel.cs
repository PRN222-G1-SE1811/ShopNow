using ShopNow.Application.DTOs.Products;

namespace ShopNow.Presentation.Models.ProductViewModel
{
    public class ProductManageViewModel
    {
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
    }

}
