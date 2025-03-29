using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopNow.Application.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace ShopNow.Application.Services.Implements
{
	public class ShippingService(IConfiguration configuration) : IShippingService
	{
		public async Task<decimal> CalculateShippingFee()
		{
			var ghnToken = configuration["Shipping:GHN:API_Token"];
			var shopId = configuration["Shipping:GHN:Shop_Id"];

			if (string.IsNullOrEmpty(ghnToken) || string.IsNullOrEmpty(shopId))
			{
				throw new Exception("GHN API Token or ShopId is missing in configuration.");
			}

			using var client = new HttpClient();

			// Thêm headers
			client.DefaultRequestHeaders.Add("Token", ghnToken);
			client.DefaultRequestHeaders.Add("ShopId", shopId); // Đảm bảo đúng chữ hoa/chữ thường

			var apiUrl = "https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee";

			// Dữ liệu gửi đi
			var requestData = new
			{
				service_type_id = 2,  // Dùng `service_type_id` thay vì `service_id`
				from_district_id = 1442,
				from_ward_code = "21211",
				to_district_id = 1820,
				to_ward_code = "030712",
				weight = 3000,
				insurance_value = 0,
			};

			// Chuyển object thành JSON
			string jsonContent = JsonConvert.SerializeObject(requestData);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			// Gửi request POST
			HttpResponseMessage response = await client.PostAsync(apiUrl, content);
			string responseData = await response.Content.ReadAsStringAsync();

			// Kiểm tra nếu request thất bại
			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"GHN API Error: {response.StatusCode} - {responseData}");
			}

			// Parse JSON phản hồi
			var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);
			if (responseJson != null && responseJson.ContainsKey("data"))
			{
				var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson["data"].ToString()!);
				if (data != null && data.ContainsKey("total"))
				{
					return Convert.ToDecimal(data["total"]);
				}
			}

			return 0;
		}
	}
}
