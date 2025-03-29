using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.DTOs.Shipping
{
	public class ShippingOrderRequest
	{
		public int service_type_id { get; set; }
		public int from_district_id { get; set; }
		public string from_ward_code { get; set; } = null!;
		public int to_district_id { get; set; }
		public string to_ward_code { get; set; } = null!;
		public int length { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public int weight { get; set; }
		public int insurance_value { get; set; }
		public string? coupon { get; set; }
		public List<Item> items { get; set; } = new();
	}
}
