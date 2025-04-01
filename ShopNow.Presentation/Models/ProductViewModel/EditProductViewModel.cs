using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Application.DTOs.Categories;
using ShopNow.Application.DTOs.Products;

namespace ShopNow.Presentation.Models.ProductViewModel
{
    public class EditProductViewModel
    {

        public EditProductDTO? EditProductDTO { get; set; }
        public SelectList? Categories { get; set; } = null!;
        public SelectList? Status { get; set; } = null!;
        public SelectList? Features { get; set; } = null!;

    }
}
