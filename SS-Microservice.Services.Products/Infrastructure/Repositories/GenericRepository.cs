using MongoDB.Driver;
using SharpCompress.Common;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Core.Entities;
using System.Collections.Generic;

namespace SS_Microservice.Services.Products.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<T> _dbSet;

        public GenericRepository(IProductContext context)
        {
            _database = context.Database;
            _dbSet = _database.GetCollection<T>(typeof(T).Name);
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
            var data = await _dbSet.Find(filter: g => g.Id == (Guid)id).SingleOrDefaultAsync();
            return data;
        }

        public async Task Insert(T entity)
        {
            await _dbSet.InsertOneAsync(entity);
        }

        public bool Update(T entity)
        {
            var updateResult = _dbSet.ReplaceOne(filter: g => g.Id == entity.Id, replacement: entity);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}