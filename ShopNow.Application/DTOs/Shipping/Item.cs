namespace ShopNow.Application.DTOs.Shipping
{
	public class Item
	{
		public string name { get; set; } = null!;
		public int quantity { get; set; }
		public int length { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public int weight { get; set; }
	}
}
