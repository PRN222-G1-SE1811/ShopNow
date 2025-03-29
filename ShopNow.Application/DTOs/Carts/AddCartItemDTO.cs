using System.ComponentModel.DataAnnotations;

namespace ShopNow.Application.DTOs.Carts
{
	public class AddCartItemDTO
	{
		[Required]
		public Guid ProductVariantId { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		public decimal Price { get; set; }
	}
}
