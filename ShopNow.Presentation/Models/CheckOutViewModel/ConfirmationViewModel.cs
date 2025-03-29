using ShopNow.Application.DTOs.CheckOut;
using ShopNow.Application.DTOs.User;

namespace ShopNow.Presentation.Models.CheckOutViewModel
{
	public class ConfirmationViewModel
	{
		public IEnumerable<CheckOutItemDTO> Items { get; set; } = null!;
		public UserDetailDTO UserDetailDTO { get; set; } = null!;
	}
}
