using System.ComponentModel.DataAnnotations;

namespace ShopNow.Application.DTOs.User
{
	public class UserDetailDTO
	{
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
		[Phone(ErrorMessage = "Invalid phone number format.")]
		public string PhoneNumber { get; set; } = null!;
	}
}
