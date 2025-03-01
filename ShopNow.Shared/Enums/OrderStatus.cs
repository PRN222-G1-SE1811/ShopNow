namespace ShopNow.Shared.Enums
{
	/// <summary>
	/// Represents the different statuses an order can have in the system.
	/// </summary>
	public enum OrderStatus
	{
		/// <summary>
		/// Order has been placed but not yet processed.
		/// </summary>
		Pending,

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
		/// Order has been canceled by the customer or the system.
		/// </summary>
		Cancelled,

		/// <summary>
		/// Order has been returned by the customer.
		/// </summary>
		Returned,

		/// <summary>
		/// Payment for the order failed.
		/// </summary>
		Failed,

		/// <summary>
		/// Order has been refunded to the customer.
		/// </summary>
		Refunded
	}
}