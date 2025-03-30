using ShopNow.Application.DTOs.CheckOut;
using ShopNow.Application.DTOs.User;

namespace ShopNow.Presentation.Models.CheckOutViewModel
{
	public class CheckOutViewModel
	{
		public List<CheckOutItemDTO> Items { get; set; } = new List<CheckOutItemDTO>();
	}
}
