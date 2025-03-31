using ShopNow.Application.DTOs.Shipping;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IShippingService
	{
		Task<decimal> CalculateShippingFee(int districtId, int wardCode);
		Task<List<Province>> GetProvinces();
		Task<List<Ward>> GetWardsByDistrict(int districtId);
		Task<List<District>> GetDistrictsByProvince(int provinceId);
	}
}
