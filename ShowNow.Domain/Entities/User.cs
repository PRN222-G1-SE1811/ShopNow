using Microsoft.AspNetCore.Identity;
using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	[Table("Users")]
	public class User : IdentityUser<Guid>, IHasCreatedAt, IHasUpdatedAt
	{
		public required string Avatar { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Address { get; set; }
		public required string City { get; set; }
		public required string Country { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
