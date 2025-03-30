using Newtonsoft.Json;

namespace ShopNow.Application.DTOs.Shipping
{
	public class District
	{
		[JsonProperty("DistrictID")]
		public int DistrictId { get; set; }

		[JsonProperty("ProvinceID")]
		public int ProvinceId { get; set; }

		[JsonProperty("DistrictName")]
		public string DistrictName { get; set; }

		[JsonProperty("Code")]
		public string Code { get; set; }

		[JsonProperty("Type")]
		public int Type { get; set; }

		[JsonProperty("SupportType")]
		public int SupportType { get; set; }

		[JsonProperty("NameExtension")]
		public List<string> NameExtension { get; set; }

		[JsonProperty("IsEnable")]
		public int IsEnable { get; set; }

		[JsonProperty("CanUpdateCOD")]
		public bool CanUpdateCod { get; set; }

		[JsonProperty("Status")]
		public int Status { get; set; }
	}

}
