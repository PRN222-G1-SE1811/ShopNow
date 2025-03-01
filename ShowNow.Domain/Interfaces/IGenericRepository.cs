using ShowNow.Domain.Entities;
using System.Linq.Expressions;

namespace ShowNow.Domain.Interfaces
{
	/// <summary>
	/// This interface is implemented by all repositories to ensure implementation of
	/// fixed methods.
	/// </summary>
	/// <typeparam name="TEntity">Main Entity type this repository works on</typeparam>
	/// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
	public interface IGenericRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
	{
		/// <summary>
		/// Gets count of all entities in this repository.
		/// </summary>
		/// <returns>Count of entities</returns>
		int Count();

		/// <summary>
		/// Gets count of all entities in this repository based on given predicate.
		/// </summary>
		/// <param name="predicate">A method to filter count</param>
		/// <returns>Count of entities</returns>
		int Count(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Gets count of all entities in this repository based on given predicate.
		/// </summary>
		/// <param name="predicate">A method to filter count</param>
		/// <returns>Count of entities</returns>
		Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Gets count of all entities in this repository.
		/// </summary>
		/// <returns>Count of entities</returns>
		Task<int> CountAsync();

		/// <summary>
		/// Deletes an entity.
		/// </summary>
		/// <param name="entity">Entity to be deleted</param>
		void Delete(TEntity entity);

		/// <summary>
		/// Deletes an entity by primary key.
		/// </summary>
		/// <param name="id">Primary key of the entity</param>
		void Delete(TPrimaryKey id);

		/// <summary>
		/// Deletes many entities by function. Notice that: All entities fits to given predicate
		/// are retrieved and deleted. This may cause major performance problems if there
		/// are too many entities with given predicate.
		/// </summary>
		/// <param name="predicate">A condition to filter entities</param>
		void Delete(Expression<Func<TEntity, bool>> predicate);


		/// <summary>
		/// Gets an entity with given given predicate or null if not found.
		/// </summary>
		/// <param name="predicate">Predicate to filter entities</param>
		/// <returns></returns>
		TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate);


		/// <summary>
		/// Gets an entity with given primary key or null if not found.
		/// </summary>
		/// <param name="id">Primary key of the entity to get</param>
		/// <returns>Entity or null</returns>
		TEntity? FirstOrDefault(TPrimaryKey id);

		/// <summary>
		/// Gets an entity with given given predicate or null if not found.
		/// </summary>
		/// <param name="predicate">Predicate to filter entities</param>
		/// <returns></returns>
		Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Gets an entity with given primary key or null if not found.
		/// </summary>
		/// <param name="id">Primary key of the entity to get</param>
		/// <returns>Entity or null</returns>
		Task<TEntity?> FirstOrDefaultAsync(TPrimaryKey id);

		/// <summary>
		/// Gets an entity with given primary key.
		/// </summary>
		/// <param name="id">Primary key of the entity to get</param>
		/// <returns>Entity</returns>
		TEntity? GetById(TPrimaryKey id);

		/// <summary>
		/// Used to get a IQueryable that is used to retrieve entities from entire table.
		/// </summary>
		/// <param name="isAll"></param>
		/// <returns>IQueryable to be used to select entities from database</returns>
		IQueryable<TEntity> GetAll(bool isAll = true);

		/// <summary>
		/// Get all with predicate
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="isAll"></param>
		/// <returns></returns>
		IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool isAll = true);

		/// <summary>
		/// Used to get a IQueryable that is used to retrieve entities from entire table.
		/// One or more
		/// </summary>
		/// <param name="propertySelectors">A list of include expressions.</param>
		/// <returns>IQueryable to be used to select entities from database</returns>
		IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

		/// <summary>
		/// Used to get all entities based on given predicate.
		/// </summary>
		/// <param name="predicate">A condition to filter entities</param>
		/// <returns>List of all entities</returns>

		List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);


		/// <summary>
		/// Used to get all entities.
		/// </summary>
		/// <returns>List of all entities</returns>
		List<TEntity> GetAllList();

		/// <summary>
		/// Used to get all entities based on given predicate.
		/// </summary>
		/// <param name="predicate">A condition to filter entities</param>
		/// <returns>List of all entities</returns>
		Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Used to get all entities.
		/// </summary>
		/// <returns>List of all entities</returns>
		Task<List<TEntity>> GetAllListAsync();

		/// <summary>
		/// Gets an entity with given primary key.
		/// </summary>
		/// <param name="id">Primary key of the entity to get</param>
		/// <returns>Entity</returns>
		Task<TEntity> GetAsync(TPrimaryKey id);

		/// <summary>
		/// Inserts a new entity.
		/// </summary>
		/// <param name="entity">Inserted entity</param>
		/// <returns>Inserted entity</returns>
		TEntity Insert(TEntity entity);

		/// <summary>
		/// Gets count of all entities in this repository based on given predicate (use this overload if expected return value is 
		/// greather than System.Int32.MaxValue).
		/// </summary>
		/// <param name="predicate">A method to filter count</param>
		/// <returns>Count of entities</returns>
		long LongCount(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Gets count of all entities in this repository (use if expected return value isgreather than System.Int32.MaxValue).
		/// </summary>
		/// <returns>Count of entities</returns>
		long LongCount();

		/// <summary>
		/// Gets count of all entities in this repository (use if expected return value is greather than System.Int32.MaxValue.
		/// </summary>
		/// <returns>Count of entities</returns>
		Task<long> LongCountAsync();

		/// <summary>
		/// Gets count of all entities in this repository based on given predicate (use this overload if expected return value is 
		/// greather than System.Int32.MaxValue).
		/// </summary>
		/// <param name="predicate">A method to filter count</param>
		/// <returns>Count of entities</returns>
		Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Gets exactly one entity with given predicate. Throws exception if no entity or
		/// more than one entity.
		/// </summary>
		/// <param name="predicate">Entity</param>
		/// <returns></returns>
		TEntity Single(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Gets exactly one entity with given predicate. Throws exception if no entity or
		/// more than one entity.
		/// </summary>
		/// <param name="predicate">Entity</param>
		/// <returns></returns>
		Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Updates an existing entity.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>Entity</returns>
		TEntity Update(TEntity entity);
	}
}
