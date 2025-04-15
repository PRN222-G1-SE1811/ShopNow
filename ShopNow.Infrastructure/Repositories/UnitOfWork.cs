using ShopNow.Infrastructure.Data;
using ShowNow.Domain.Entities;
using ShowNow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.Infrastructure.Repositories
{
	public class UnitOfWork<TEntity, TPrimaryKey>(ShopNowDbContext shopNowDbContext) : IUnitOfWork<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
	{
		private IGenericRepository<TEntity, TPrimaryKey>? _genericRepository;

		public IGenericRepository<TEntity, TPrimaryKey> GenericRepository
		{
			get
			{
				if (_genericRepository == null)
				{
					_genericRepository = new GenericRepository<TEntity, TPrimaryKey>(shopNowDbContext);
				}
				return _genericRepository;
			}
		}

		public int Commit()
		{
			return shopNowDbContext.SaveChanges();
		}

		public async Task<int> CommitAsync()
		{
			return await shopNowDbContext.SaveChangesAsync();
		}
        public async Task<int> SaveChangesAsync()
        {
            return await shopNowDbContext.SaveChangesAsync();
        }
    }
}
