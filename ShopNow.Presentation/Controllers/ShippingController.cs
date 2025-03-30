using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Interfaces;

namespace ShopNow.Presentation.Controllers
{
	public class ShippingController(IShippingService shippingService) : Controller
	{
		[HttpPost("Provinces")]
		public async Task<IActionResult> GetProvinces() => Json(await shippingService.GetProvinces());

		[HttpPost("Districts/{provinceCode:int}")]
		public async Task<IActionResult> GetDistricts([FromRoute] int provinceCode) => Json(await shippingService.GetDistrictsByProvince(provinceCode));

		[HttpPost("Wards/{districtCode:int}")]
		public async Task<IActionResult> GetWards([FromRoute] int districtCode) => Json(await shippingService.GetWardsByDistrict(districtCode));

		[HttpPost("Fee")]
		public async Task<IActionResult> GetShippingFee([FromQuery] int districtCode, [FromQuery] int wardCode) => Json(await shippingService.CalculateShippingFee(districtCode, wardCode));
	}
}
