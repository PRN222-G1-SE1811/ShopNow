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

		[BindNever]
		public ProductStatus Status { get; set; }

		[Required]
		public ProductFeatured Featured { get; set; }

		[Required]
		public Guid CategoryId { get; set; }

		[BindNever]
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
