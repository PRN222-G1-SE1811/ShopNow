using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShopNow.Application.Attributes;
using ShopNow.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ShopNow.Application.DTOs.Prodducts
{
	public class CreateProductDTO
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required(ErrorMessage = "Please enter a value greater than or equal to 0.")]
		[Range(0, double.MaxValue, ErrorMessage = "Please enter a value greater than or equal to 0.")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Please enter a value greater than or equal to 0.")]
		[Range(0, 100, ErrorMessage = "Please enter a value greater than or equal to 0.")]
		public float Discount { get; set; }

		[Required(ErrorMessage = "Please enter a value greater than or equal to 0.")]
		[Range(0, int.MaxValue, ErrorMessage = "Please enter a value greater than or equal to 0.")]
		public int Quantity { get; set; }

		[BindNever]
		public ProductStatus Status { get; set; }

		[Required]
		public ProductFeatured Featured { get; set; }

		[Required]
		[RequiredFile]
		public IEnumerable<IFormFile> Files { get; set; } = new List<IFormFile>();

		[Required]
		public Guid CategoryId { get; set; }

		[BindNever]
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
