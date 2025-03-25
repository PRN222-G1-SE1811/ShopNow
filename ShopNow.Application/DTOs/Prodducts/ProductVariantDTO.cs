using System.ComponentModel.DataAnnotations;

namespace ShopNow.Application.DTOs.Prodducts
{
	public class ProductVariantDTO
	{
		[Required]
		public ProductAttributeDTO ProductDetail { get; set; }

		[Required]
		public List<AttributeDTO> AttributeDTOs { get; set; } = new List<AttributeDTO>() { new AttributeDTO() };
	}
}
