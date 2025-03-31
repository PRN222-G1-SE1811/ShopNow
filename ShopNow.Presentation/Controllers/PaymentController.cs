using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Implements;
using ShopNow.Application.Services.Interfaces;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using VNPAY.NET.Utilities;

namespace ShopNow.Presentation.Controllers
{
	public class PaymentController : Controller
	{

		private readonly IVnpay _vnpay;
		private readonly IConfiguration _configuration;
		private readonly IOrderService _orderService;

		public PaymentController(IVnpay vnPayservice, IConfiguration configuration, IOrderService orderService)
		{
			_vnpay = vnPayservice;
			_configuration = configuration;
			_orderService = orderService;
			_vnpay.Initialize(_configuration["Vnpay:TmnCode"]!, _configuration["Vnpay:HashSecret"]!, _configuration["Vnpay:BaseUrl"]!, _configuration["Vnpay:CallbackUrl"]!);
		}

		[HttpGet("CreatePaymentUrl/{orderId:guid}")]
		public IActionResult CreatePaymentUrl([FromRoute] Guid orderId)
		{
			var order = _orderService.GetOrderDetail(orderId);
			try
			{
				var ipAddress = NetworkHelper.GetIpAddress(HttpContext);

				var request = new PaymentRequest
				{
					PaymentId = DateTime.Now.Ticks,
					Money = (double)(order.TotalCost),
					Description = $"Thanh toan don hang thoi gian: {order.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")} - Ma don hang: {orderId}",
					IpAddress = ipAddress,
					BankCode = BankCode.ANY,
					CreatedDate = DateTime.Now,
					Currency = Currency.VND,
					Language = DisplayLanguage.Vietnamese
				};

				var paymentUrl = _vnpay.GetPaymentUrl(request);

				return Redirect(paymentUrl);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("IpnAction")]
		public IActionResult IpnAction()
		{
			if (Request.QueryString.HasValue)
			{
				try
				{
					var paymentResult = _vnpay.GetPaymentResult(Request.Query);

					// Trích xuất orderId từ Description
					string description = paymentResult.PaymentResponse.Description;
					Guid orderId;

					// Parse orderId từ description
					int orderIdIndex = description.IndexOf("Ma don hang: ");
					if (orderIdIndex >= 0)
					{
						string orderIdStr = description.Substring(orderIdIndex + 13).Trim();
						if (Guid.TryParse(orderIdStr, out orderId))
						{
							if (paymentResult.IsSuccess)
							{
								// Cập nhật trạng thái đơn hàng thành công
								_orderService.UpdateOrderStatus(orderId, Shared.Enums.OrderStatus.Paid);
								return Ok();
							}
							// Cập nhật trạng thái đơn hàng thất bại
							_orderService.UpdateOrderStatus(orderId, Shared.Enums.OrderStatus.Failed);
						}
					}
				}
				catch (Exception ex)
				{
					return RedirectToAction("PaymentFailed", "CheckOut");
				}
			}
			return RedirectToAction("PaymentFailed", "CheckOut");
		}

		[HttpGet("Callback")]
		public ActionResult<string> Callback()
		{
			if (Request.QueryString.HasValue)
			{
				try
				{
					var paymentResult = _vnpay.GetPaymentResult(Request.Query);
					var resultDescription = $"{paymentResult.PaymentResponse.Description}. {paymentResult.TransactionStatus.Description}.";

					if (paymentResult.IsSuccess)
					{
						return View(resultDescription);
					}

					return View(resultDescription);
				}
				catch (Exception ex)
				{
					return View(ex.Message);
				}
			}

			return RedirectToAction("PaymentFailed", "CheckOut");
		}
	}
}
