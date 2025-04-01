using ShowNow.Domain.Entities;

namespace ShowNow.Domain.Interfaces
{
	public interface IUnitOfWork<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
	{
		public IGenericRepository<TEntity, TPrimaryKey> GenericRepository { get; }
		public Task<int> CommitAsync();
		public int Commit();
		public Task<int> SaveChangesAsync();
	}
}
