using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Application.DTOs.Categories;
using ShopNow.Application.DTOs.Products;

namespace ShopNow.Presentation.Models.ProductViewModel
{
	public class CreateProductViewModel
	{
		public CreateProductDTO CreateProductDTO { get; set; } = null!;
		public SelectList Categories { get; set; } = null!;
		public SelectList Status { get; set; } = null!;
		public SelectList Features { get; set; } = null!;
	}
}
