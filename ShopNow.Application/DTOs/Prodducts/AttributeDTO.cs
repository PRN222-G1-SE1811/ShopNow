using System.ComponentModel.DataAnnotations;

namespace ShopNow.Application.DTOs.Prodducts
{
	public class AttributeDTO
	{
		[RegularExpression("^(color|size|weight)$", ErrorMessage = "Invalid attribute type.")]
		public string Key { get; set; } = null!;

		[Required]
		public string Value { get; set; } = null!;
	}
}
