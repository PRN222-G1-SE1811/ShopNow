namespace ShopNow.Shared.Enums
{
	/// <summary>
	/// Represents the different types of payment methods available in the system.
	/// </summary>
	public enum PaymentMethod
	{
		/// <summary>
		/// Payment made through a bank transfer.
		/// </summary>
		VNPay,

		/// <summary>
		/// Payment made in cash upon delivery of the order.
		/// </summary>
		CashOnDelivery,
	}
}
