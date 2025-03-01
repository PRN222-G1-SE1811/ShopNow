namespace ShowNow.Domain.Interfaces
{
	public interface IHasDeletedAt
	{
		public DateTime? DeletedAt { get; set; }
	}
}
