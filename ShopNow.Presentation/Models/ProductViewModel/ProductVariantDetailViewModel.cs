using ShopNow.Application.DTOs.Products;
using ShowNow.Domain.Entities;

namespace ShopNow.Presentation.Models.ProductViewModel
{
    public class ProductVariantDetailViewModel
    {
        public ProductVariant ProductVariant { get; set; }
        public List<AssetDTO> Assets { get; set; }
    }
}
