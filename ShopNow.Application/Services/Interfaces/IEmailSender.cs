using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Application.Services.Interfaces
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
		Task SendSmsAsync(string number, string message);
	}
}
