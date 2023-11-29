using Microsoft.EntityFrameworkCore;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Basket.Infrastructure.Data.DBContext;

namespace SS_Microservice.Services.Basket.Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly BasketDBContext _dbContext;
		private readonly DbSet<T> _entities;

		public GenericRepository(BasketDBContext dbContext)
		{
			_dbContext = dbContext;
			_entities = dbContext.Set<T>();
		}

		public bool Delete(T entity)
		{
			if (entity == null)
			{
				throw new NotFoundException("Cannot find this entity");
			}
			try
			{
				var x = _entities.Remove(entity);
				return x.State == EntityState.Deleted;
			}
			catch
			{
				throw new Exception("Cannot delete this entity");
			}
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await _entities.ToListAsync();
		}

		public async Task<T> GetById(object id)
		{
			return await _entities.FindAsync(id) ?? throw new NotFoundException("Cannot find this entity");
		}

		public async Task<bool> Insert(T entity)
		{
			if (entity == null)
			{
				throw new NotFoundException("Cannot find this entity");
			}
			try
			{
				var x = await _entities.AddAsync(entity);
				return x.State == EntityState.Added;
			}
			catch
			{
				throw new Exception("Cannot insert this entity");
			}
		}

		public bool Update(T entity)
		{
			if (entity == null)
			{
				throw new NotFoundException("Cannot find this entity");
			}
			try
			{
				_dbContext.Entry(entity).State = EntityState.Modified;
				return true;
			}
			catch
			{
				throw new Exception("Cannot update this entity");
			}
		}

		// Specification Pattern
		public async Task<List<T>> ListAsync(ISpecifications<T> specification)
		{
			return await ApplySpecification(specification).ToListAsync();
		}

		public async Task<T> GetEntityWithSpec(ISpecifications<T> specification)
		{
			return await ApplySpecification(specification).FirstOrDefaultAsync();
		}

		public async Task<int> CountAsync(ISpecifications<T> specifications)
		{
			return await ApplySpecification(specifications).CountAsync();
		}

		private IQueryable<T> ApplySpecification(ISpecifications<T> specifications)
		{
			return SpecificationEvaluator<T>.GetQuery(_entities.AsQueryable(), specifications);
		}
	}
}