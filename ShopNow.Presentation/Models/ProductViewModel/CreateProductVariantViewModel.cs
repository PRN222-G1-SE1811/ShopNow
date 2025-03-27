using ShopNow.Application.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace ShopNow.Presentation.Models.ProductViewModel
{
	public class CreateProductVariantViewModel
	{
		[Required(ErrorMessage = "Product is required.")]
		public Guid ProductId { get; set; }
		public List<CreateProductVariantDTO> ProductVariantDTOs { get; set; } = new List<CreateProductVariantDTO>();

	}
}
