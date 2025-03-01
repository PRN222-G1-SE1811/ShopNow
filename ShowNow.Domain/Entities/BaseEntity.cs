using System.ComponentModel.DataAnnotations;

namespace ShowNow.Domain.Entities
{
	public abstract class BaseEntity<TKey>
	{
		[Key]
		public required TKey Id { get; set; }
	}
}
