namespace ShopNow.Shared.Enums
{
	/// <summary>
	/// Represents the different statuses an order can have in the system.
	/// </summary>
	public enum OrderStatus
	{
		/// <summary>
		/// Order is currently being processed.
		/// </summary>
		Processing,

		/// <summary>
		/// Order has been shipped to the customer.
		/// </summary>
		Shipped,

		/// <summary>
		/// Order has been successfully delivered to the customer.
		/// </summary>
		Delivered,

		/// <summary>
		/// Payment for the order failed.
		/// </summary>
		Failed,
	}
}