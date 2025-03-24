using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopNow.Application.Attributes
{
	public class RequiredFileAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var files = value as IEnumerable<IFormFile>;
			if (files == null || !files.Any() || files.All(f => f.Length == 0))
			{
				return new ValidationResult("Bạn phải chọn ít nhất một tệp.");
			}
			return ValidationResult.Success;
		}
	}
}
