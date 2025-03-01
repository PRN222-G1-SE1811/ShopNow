using Microsoft.AspNetCore.Identity;
using ShowNow.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowNow.Domain.Entities
{
	[Table("Users")]
	public class User : IdentityUser<Guid>, IHasCreatedAt, IHasUpdatedAt
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
