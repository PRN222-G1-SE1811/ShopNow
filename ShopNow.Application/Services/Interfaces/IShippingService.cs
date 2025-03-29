namespace ShopNow.Application.Services.Interfaces
{
	public interface IShippingService
	{
		Task<decimal> CalculateShippingFee();
	}
}
