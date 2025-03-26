using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Application.DTOs.Prodducts;

namespace ShopNow.Presentation.Models.ProductViewModel
{
	public class CreateProductBaseInfoViewModel
	{
		public SelectList Categories { get; set; }
		public SelectList Featured { get; set; }
		public CreateProductDTO CreateProductDTO { get; set; }
	}
}
