using Microsoft.EntityFrameworkCore;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Specifications;

namespace SS_Microservice.Common.Repository
{
    public class GenericRepository<Entity, Context> : IGenericRepository<Entity> where Entity : class where Context : DbContext
    {
        private readonly Context _dbContext;
        private readonly DbSet<Entity> _entities;

        public GenericRepository(Context dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<Entity>();
        }

        public bool Delete(Entity entity)
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

        public async Task<IEnumerable<Entity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<Entity> GetById(object id)
        {
            return await _entities.FindAsync(id) ?? throw new NotFoundException("Cannot find this entity");
        }

        public async Task<bool> Insert(Entity entity)
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

        public bool Update(Entity entity)
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
        public async Task<List<Entity>> ListAsync(ISpecifications<Entity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<Entity> GetEntityWithSpec(ISpecifications<Entity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecifications<Entity> specifications)
        {
            return await ApplySpecification(specifications).CountAsync();
        }

        private IQueryable<Entity> ApplySpecification(ISpecifications<Entity> specifications)
        {
            return SpecificationEvaluator<Entity>.GetQuery(_entities.AsQueryable(), specifications);
        }
    }
}
