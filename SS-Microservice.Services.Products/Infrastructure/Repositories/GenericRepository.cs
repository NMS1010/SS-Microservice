using MongoDB.Driver;
using SharpCompress.Common;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Domain.Entities;
using System.Collections.Generic;

namespace SS_Microservice.Services.Products.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseMongoEntity
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<T> _dbSet;
        private readonly ICurrentUserService _currentUserService;

        public GenericRepository(IProductContext context, ICurrentUserService currentUserService)
        {
            _database = context.Database;
            _dbSet = _database.GetCollection<T>(typeof(T).Name);
            _currentUserService = currentUserService;
        }

        public Task<int> CountAsync(ISpecifications<T> specifications)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity)
        {
            DeleteResult deleteResult = _dbSet.DeleteOne(filter: g => g.Id == entity.Id);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var all = await _dbSet.FindAsync(x => true);
            return all.ToEnumerable();
        }

        public async Task<T> GetById(object id)
        {
            var data = await _dbSet.Find(filter: g => g.Id == id.ToString()).SingleOrDefaultAsync();
            return data;
        }

        public Task<T> GetEntityWithSpec(ISpecifications<T> specification)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(T entity)
        {
            try
            {
                var now = DateTime.Now;
                entity.CreatedDate = now;
                entity.UpdatedDate = now;
                entity.CreatedBy = _currentUserService?.UserId ?? "system";
                entity.UpdatedBy = _currentUserService?.UserId ?? "system";
                await _dbSet.InsertOneAsync(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<List<T>> ListAsync(ISpecifications<T> specification)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            var now = DateTime.Now;
            entity.UpdatedDate = now;
            entity.UpdatedBy = _currentUserService?.UserId ?? "system";
            var updateResult = _dbSet.ReplaceOne(filter: g => g.Id == entity.Id, replacement: entity);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}