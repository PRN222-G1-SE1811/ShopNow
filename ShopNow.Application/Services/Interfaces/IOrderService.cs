using ShopNow.Application.DTOs.CheckOut;
using ShopNow.Application.DTOs.User;
using ShopNow.Shared.Enums;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IOrderService
	{
		Task CreateOrder(List<CheckOutItemDTO> items, UserDetailDTO userDetail, PaymentMethod paymentMethod, decimal shippingFee);
	}
}
