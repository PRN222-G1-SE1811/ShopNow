using ShopNow.Application.DTOs.Shipping;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IShippingService
	{
		Task<decimal> CalculateShippingFee();
		Task<List<Province>> GetProvinces();
	}
}
