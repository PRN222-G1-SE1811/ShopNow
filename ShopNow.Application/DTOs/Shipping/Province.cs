namespace ShopNow.Application.DTOs.Shipping
{
	public class Province
	{
		public int ProvinceID { get; set; }
		public string ProvinceName { get; set; }
		public int CountryID { get; set; }
		public int Code { get; set; }
		public List<string> NameExtension { get; set; }
		public int IsEnable { get; set; }
		public int RegionID { get; set; }
		public int UpdatedBy { get; set; }
		public string CanUpdateCOD { get; set; }
		public int Status { get; set; }
	}
}
