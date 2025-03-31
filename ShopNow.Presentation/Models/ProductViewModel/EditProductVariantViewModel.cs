using ShopNow.Application.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace ShopNow.Presentation.Models.ProductViewModel
{
    public class EditProductVariantViewModel
    {
        public List<EditProductVariantDTO> ProductVariantDTOs { get; set; } = new List<EditProductVariantDTO>();
    }

}
