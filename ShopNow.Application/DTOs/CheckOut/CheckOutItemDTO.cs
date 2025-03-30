using System.ComponentModel.DataAnnotations;

namespace ShopNow.Application.DTOs.CheckOut
{
	public class CheckOutItemDTO
	{
		[Required]
		public Guid ProductVariantId { get; set; }

		[Required]
		public string ProductName { get; set; } = null!;

		[Required]
		public int Quantity { get; set; }

		[Required]
		public decimal Price { get; set; }
	}
}
