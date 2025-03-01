namespace ShopNow.Shared.Enums
{
	/// <summary>
	/// Represents the different types of payment methods available in the system.
	/// </summary>
	public enum PaymentMethod
	{
		/// <summary>
		/// Payment made using a credit card.
		/// </summary>
		CreditCard,

		/// <summary>
		/// Payment processed via PayPal.
		/// </summary>
		Paypal,

		/// <summary>
		/// Payment made through a bank transfer.
		/// </summary>
		BankTranfer,

		/// <summary>
		/// Payment made in cash upon delivery of the order.
		/// </summary>
		CashOnDelivery,
	}
}
