using Microsoft.AspNetCore.Mvc;
using ShopNow.Application.Services.Implements;
using ShopNow.Application.Services.Interfaces;
using System.Security.Claims;
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
		private readonly IEmailSender _emailSender;

		public PaymentController(IVnpay vnPayservice, IConfiguration configuration, IOrderService orderService, IEmailSender emailSender)
		{
			_vnpay = vnPayservice;
			_configuration = configuration;
			_orderService = orderService;
			_emailSender = emailSender;
			_vnpay.Initialize(_configuration["Vnpay:TmnCode"]!, _configuration["Vnpay:HashSecret"]!, _configuration["Vnpay:BaseUrl"]!, _configuration["Vnpay:CallbackUrl"]!);
		}

		[HttpGet("CreatePaymentUrl/{orderId:guid}")]
		public IActionResult CreatePaymentUrl([FromRoute] Guid orderId)
		{
			var order = _orderService.GetOrderDetail(orderId);
			if (order.PaymentMethod == Shared.Enums.PaymentMethod.CashOnDelivery || order.OrderStatus == Shared.Enums.OrderStatus.Paid)
			{
				return RedirectToAction("Index", "Home");
			}
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
					string description = paymentResult.Description;
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
								var email = User.FindFirstValue(ClaimTypes.Email);
								_orderService.UpdateOrderStatus(orderId, Shared.Enums.OrderStatus.Paid);
								_emailSender.SendEmailAsync(email!, GetRandomEmailTitle(), GetContent(orderId));
								return RedirectToAction("PaymentSuccesed", "CheckOut");
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

		private string GetRandomEmailTitle()
		{
			List<string> emailTitles = new List<string>
			{
				"You Just Made Our Day! 🎉 Thanks for Shopping with Us!",
				"You're Awesome! Thanks for Your Purchase! 🚀",
				"High Five! 🖐️ Your Order is on the Way!",
				"Mission Accomplished! 🛍️ Thanks for Shopping with Us!",
				"We Like Your Style! Thanks for Your Order! 😎",
				"Guess What? You Just Got Something Awesome! 🎁",
				"Shopping Level: Expert! 🏆 Thanks for Your Purchase!",
				"Your Order is in Good Hands! 👐 Thanks a Bunch!",
				"Cha-Ching! 💰 You Just Scored Big!",
				"Order Confirmed! Now the Exciting Part Begins! 🎊"
			};

			Random random = new Random();
			int index = random.Next(emailTitles.Count);
			return emailTitles[index];
		}

		private string GetContent(Guid orderId)
		{
			return $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Thank You!</title>\r\n</head>\r\n<body style=\"font-family: Arial, sans-serif; text-align: center; background-color: #f9f9f9; padding: 20px;\">\r\n\r\n    <div style=\"max-width: 600px; margin: 0 auto; background: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0px 4px 10px rgba(0,0,0,0.1);\">\r\n        <h1 style=\"color: #ff6f61;\">🎉 Thank You for Your Purchase! 🎉</h1>\r\n        <p style=\"font-size: 18px; color: #333;\">You just made our day! Your order is on its way, and we can’t wait for you to enjoy your new items.</p>\r\n        <p style=\"font-size: 16px; color: #555;\">Need help? Our friendly support team is here for you! 🚀</p>\r\n        <a href=\"Order/{orderId}\" style=\"display: inline-block; background: #ff6f61; color: white; padding: 12px 24px; font-size: 16px; text-decoration: none; border-radius: 5px; margin-top: 10px;\">Track Your Order</a>\r\n    </div>\r\n\r\n</body>\r\n</html>\r\n";
		}
	}
}
