using Microsoft.EntityFrameworkCore;
using ShopNow.Infrastructure.Data;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Infrastructure.Repositories
{
	/// <summary>
	/// Serves as a generic base class for concrete repositories to support basic CRUD oprations on data in the system.
	/// </summary>
	/// <typeparam name="TEntity">The type of entity this repository works with. Must be a class inheriting DomainEntity</typeparam>
	/// <typeparam name="TPrimaryKey">The type of primary key</typeparam>
	public class GenericRepository<TEntity, TPrimaryKey> : IGenericRepository<TEntity, TPrimaryKey>, IDisposable where TEntity : BaseEntity<TPrimaryKey>
	{
		protected readonly ShopNowDbContext DbContext;

		public GenericRepository(ShopNowDbContext dbContext)
		{
			DbContext = dbContext;
		}

		public int Count()
		{
			return GetAll().Count();
		}

		public int Count(Expression<Func<TEntity, bool>> predicate)
		{
			return GetAll().Count(predicate);
		}

		public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await GetAll().CountAsync(predicate);
		}

		public async Task<int> CountAsync()
		{
			return await GetAll().CountAsync();
		}

		public void Delete(TEntity entity)
		{
			DbContext.Set<TEntity>().Remove(entity);
		}

		public void Delete(TPrimaryKey id)
		{
			DbContext.Set<TEntity>().Remove(GetById(id));
		}

		public void Delete(Expression<Func<TEntity, bool>> predicate)
		{
			DbContext.Set<TEntity>().RemoveRange(GetAll().Where(predicate));
		}

		public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
		{
			return GetAll().FirstOrDefault(predicate);
		}

		public TEntity? FirstOrDefault(TPrimaryKey id)
		{
			return DbContext.Set<TEntity>().FirstOrDefault(x => x.Id.Equals(id));
		}

		public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await GetAll().FirstOrDefaultAsync(predicate);
		}

		public async Task<TEntity?> FirstOrDefaultAsync(TPrimaryKey id)
		{
			return await GetAll().FirstOrDefaultAsync(x => x.Id.Equals(id));
		}

		public TEntity? GetById(TPrimaryKey id)
		{
			return DbContext.Set<TEntity>().Find(id);
		}

		public IQueryable<TEntity> GetAll(bool isAll = true)
		{
			return DbContext.Set<TEntity>().AsQueryable();
		}

		public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
		{
			IQueryable<TEntity> items = GetAll();

			if (propertySelectors != null)
			{
				foreach (var includeProperty in propertySelectors)
				{
					items = items.Include(includeProperty);
				}
			}
			return items;
		}

		public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
		{
			return GetAll().Where(predicate).ToList();
		}

		public List<TEntity> GetAllList()
		{
			return GetAll().ToList();
		}

		public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await GetAll().Where(predicate).ToListAsync();
		}

		public async Task<List<TEntity>> GetAllListAsync()
		{
			return await GetAll().ToListAsync();
		}

		public async Task<TEntity?> GetAsync(TPrimaryKey id)
		{
			return await DbContext.Set<TEntity>().FindAsync(id);
		}

		public TEntity Insert(TEntity entity)
		{
			return DbContext.Set<TEntity>().Add(entity).Entity;
		}

		public TPrimaryKey InsertAndGetId(TEntity entity)
		{
			var result = DbContext.Set<TEntity>().Add(entity);
			return result.Entity.Id;
		}

		public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
		{
			var result = await DbContext.Set<TEntity>().AddAsync(entity);
			return result.Entity.Id;
		}

		public async Task<TEntity> InsertAsync(TEntity entity)
		{
			var result = await DbContext.Set<TEntity>().AddAsync(entity);
			return result.Entity;
		}

		public long LongCount(Expression<Func<TEntity, bool>> predicate)
		{
			return GetAll().LongCount(predicate);
		}

		public long LongCount()
		{
			return GetAll().LongCount();
		}

		public async Task<long> LongCountAsync()
		{
			return await GetAll().LongCountAsync();
		}

		public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await GetAll().LongCountAsync(predicate);
		}

		public TEntity Single(Expression<Func<TEntity, bool>> predicate)
		{
			return GetAll().Single(predicate);
		}

		public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await GetAll().SingleAsync(predicate);
		}

		public TEntity Update(TEntity entity)
		{
			var result = DbContext.Set<TEntity>().Update(entity);
			return result.Entity;
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					DbContext.Dispose();
				}
				disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool isAll = true)
		{
			return GetAll(isAll).Where(predicate);
		}
	}
}
