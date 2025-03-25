using ShopNow.Application.Attributes;
using ShopNow.Application.DTOs.Prodducts;
using System.ComponentModel.DataAnnotations;

namespace ShopNow.Presentation.Models.ProductViewModel
{
	public class CreateAttributeViewModel
	{
		public List<ProductVariantDTO> ProductVariantDTOs { get; set; }
	}
}
