using Microsoft.AspNetCore.Http;
using ShopNow.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ShopNow.Application.DTOs.Prodducts
{
	public class ProductAttributeDTO
	{
		[Required(ErrorMessage = "Please enter a value greater than or equal to 0.")]
		[Range(0, double.MaxValue, ErrorMessage = "Please enter a value greater than or equal to 0.")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Please enter a value greater than or equal to 0.")]
		[Range(0, 100, ErrorMessage = "Please enter a value greater than or equal to 0.")]
		public float Discount { get; set; }

		[Required(ErrorMessage = "Please enter a value greater than or equal to 0.")]
		[Range(0, int.MaxValue, ErrorMessage = "Please enter a value greater than or equal to 0.")]
		public int Quantity { get; set; }

		[Required]
		[RequiredFile]
		public IEnumerable<IFormFile> Files { get; set; } = new List<IFormFile>();
	}
}
