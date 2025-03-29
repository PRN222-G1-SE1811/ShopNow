namespace ShopNow.Application.DTOs.Carts
{
	public class CartDTO
	{
		public IEnumerable<CartItemDTO> CartItems { get; set; } = null!;
	}
}
