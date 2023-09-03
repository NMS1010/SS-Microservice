using Microsoft.EntityFrameworkCore;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
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
                _entities.Remove(entity);
                return _dbContext.SaveChanges() > 0;
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
                await _entities.AddAsync(entity);
                return await _dbContext.SaveChangesAsync() > 0;
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
                return _dbContext.SaveChanges() > 0;
            }
            catch
            {
                throw new Exception("Cannot update this entity");
            }
        }
    }
}