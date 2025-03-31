using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ShopNow.Application.DTOs.User
{
	public class UserDetailDTO
	{
		[BindNever]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "First name is required.")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Last name is required.")]
		public string LastName { get; set; } = null!;

		[Required(ErrorMessage = "Address is required.")]
		public string Address { get; set; } = null!;

		[Required(ErrorMessage = "City is required.")]
		public string City { get; set; } = null!;

		[Required(ErrorMessage = "District is required.")]
		public string District { get; set; } = null!;

		[Required(ErrorMessage = "Ward is required.")]
		public string Ward { get; set; } = null!;

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string Email { get; set; } = null!;

		[Required(ErrorMessage = "Phone number is required.")]
		[RegularExpression(@"^(0|\+84)(3[2-9]|5[2689]|7[06-9]|8[1-9]|9[0-9])\d{7}$",
		ErrorMessage = "Invalid Vietnamese phone number format.")]
		public string PhoneNumber { get; set; } = null!;
	}
}
