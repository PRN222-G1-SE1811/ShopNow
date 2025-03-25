using System.ComponentModel.DataAnnotations;

namespace ShowNow.Domain.Entities
{
	public abstract class BaseEntity<TKey>
	{
		[Key]
		public TKey Id { get; set; }
	}
}
