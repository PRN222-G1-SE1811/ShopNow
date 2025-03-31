using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.DTOs.Shipping
{
	public class Ward
	{
		[JsonProperty("WardCode")]
		public string WardCode { get; set; }

		[JsonProperty("DistrictID")]
		public int DistrictId { get; set; }

		[JsonProperty("WardName")]
		public string WardName { get; set; }

		[JsonProperty("NameExtension")]
		public List<string> NameExtension { get; set; }

		[JsonProperty("CanUpdateCOD")]
		public bool CanUpdateCod { get; set; }

		[JsonProperty("SupportType")]
		public int SupportType { get; set; }

		[JsonProperty("Status")]
		public int Status { get; set; }
	}

}
