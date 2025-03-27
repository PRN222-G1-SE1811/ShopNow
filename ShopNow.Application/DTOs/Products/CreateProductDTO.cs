using ShopNow.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ShopNow.Application.DTOs.Products
{
	public class CreateProductDTO
	{
		[Required(ErrorMessage = "Product name is required.")]
		public required string Name { get; set; }

		[Required(ErrorMessage = "CategoryId is required.")]
		public Guid CategoryId { get; set; }

		[Required(ErrorMessage = "Description is required.")]
		public required string Description { get; set; }

		[Required(ErrorMessage = "Product status is required.")]
		public required ProductStatus Status { get; set; }

		[Required(ErrorMessage = "Product featured status is required.")]
		public required ProductFeatured Featured { get; set; }
	}
}
