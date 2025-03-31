using AutoMapper;
using ShopNow.Application.DTOs.CheckOut;
using ShopNow.Application.DTOs.Orders;
using ShopNow.Application.DTOs.User;
using ShopNow.Application.Services.Interfaces;
using ShopNow.Shared.Enums;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;

namespace ShopNow.Application.Services.Implements
{
	public class OrderService(IUnitOfWork<Order, Guid> unitOfWork, IMapper mapper, IShippingService shippingService, IProductVariantService productVariantService) : IOrderService
	{
		public async Task<Guid> CreateOrder(List<CheckOutItemDTO> items, UserDetailDTO userDetail, PaymentMethod paymentMethod, decimal shippingFee)
		{
			var id = await InsertOrder(items, userDetail, paymentMethod, shippingFee);
			return id;
		}

		public OrderDTO GetOrderDetail(Guid id)
		{
			var order = unitOfWork.GenericRepository.GetById(id);
			return mapper.Map<OrderDTO>(order);
		}

		public async Task<bool> UpdateOrderStatus(Guid id, OrderStatus status)
		{
			var order = unitOfWork.GenericRepository.GetById(id);
			order.OrderStatus = status;
			unitOfWork.GenericRepository.Update(order);
			return await unitOfWork.CommitAsync() > 0;
		}

		private async Task<Guid> InsertOrder(List<CheckOutItemDTO> items, UserDetailDTO userDetail, PaymentMethod paymentMethod, decimal shippingFee)
		{
			decimal totalCost = items.Sum(item => item.Price) + shippingFee;

			// Batch update to decrease product quantity (if applicable)
			foreach (var item in items)
			{
				await productVariantService.DecreaseQuantity(item.ProductVariantId, item.Quantity);
			}

			// Fetch address components efficiently
			var provinceName = (await shippingService.GetProvinces())
				.FirstOrDefault(p => p.ProvinceID == int.Parse(userDetail.City))?.ProvinceName ?? string.Empty;

			var districtName = (await shippingService.GetDistrictsByProvince(int.Parse(userDetail.City)))
				.FirstOrDefault(d => d.DistrictId == int.Parse(userDetail.District))?.DistrictName ?? string.Empty;

			var wardName = (await shippingService.GetWardsByDistrict(int.Parse(userDetail.District)))
				.FirstOrDefault(w => w.WardCode == userDetail.Ward)?.WardName ?? string.Empty;

			var address = $"{userDetail.Address}/{wardName}/{districtName}/{provinceName}";

			var order = new Order()
			{
				CustomerId = userDetail.Id,
				CreatedAt = DateTime.UtcNow,
				OrderStatus = OrderStatus.Processing,
				PaymentMethod = paymentMethod,
				ShipFee = shippingFee,
				ShippingAddress = address,
				TotalCost = totalCost,
				OrderItems = mapper.Map<List<OrderItem>>(items)
			};

			unitOfWork.GenericRepository.Insert(order);
			await unitOfWork.CommitAsync();
			return order.Id;
		}

	}
}
