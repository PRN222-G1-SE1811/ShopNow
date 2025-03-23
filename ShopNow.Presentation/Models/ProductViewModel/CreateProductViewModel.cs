using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNow.Application.DTOs.Prodducts;

namespace ShopNow.Presentation.Models.ProductViewModel
{
	public class CreateProductViewModel
	{
		public SelectList Categories { get; set; }
		public SelectList Statuses { get; set; }
		public SelectList Featured { get; set; }
		public CreateProductDTO CreateProductDTO { get; set; }
	}
}
