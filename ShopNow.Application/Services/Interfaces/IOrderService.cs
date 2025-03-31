using ShopNow.Application.DTOs.CheckOut;
using ShopNow.Application.DTOs.Orders;
using ShopNow.Application.DTOs.User;
using ShopNow.Shared.Enums;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IOrderService
	{
		Task<Guid> CreateOrder(List<CheckOutItemDTO> items, UserDetailDTO userDetail, PaymentMethod paymentMethod, decimal shippingFee);
		OrderDTO GetOrderDetail(Guid id);
		Task<bool> UpdateOrderStatus(Guid id, OrderStatus status);
	}
}
