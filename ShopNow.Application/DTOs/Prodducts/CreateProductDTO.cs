using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShopNow.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.DTOs.Prodducts
{
	public class CreateProductDTO
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		[Required]
		[Range(0, 100)]
		public float Discount { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int Quantity { get; set; }

		[Required]
		public ProductStatus Status { get; set; }

		[Required]
		public ProductFeatured Featured { get; set; }

		[Required]
		public IEnumerable<IFormFile> Files { get; set; }

		[Required]
		public Guid CategoryId { get; set; }

		[BindNever]
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
