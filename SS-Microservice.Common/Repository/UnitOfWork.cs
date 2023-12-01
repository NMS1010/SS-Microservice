using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;

namespace SS_Microservice.Common.Repository
{
    internal class UnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private Hashtable _repositories;

        public T Context { get; }

        public UnitOfWork(T context)
        {
            Context = context;
        }

        private IDbContextTransaction _objTran;

        public IGenericRepository<Entity> Repository<Entity>() where Entity : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(Entity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<Entity, T>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(Entity)), Context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<Entity>)_repositories[type];
        }

        public async Task Commit()
        {
            await _objTran.CommitAsync();
        }

        public async Task CreateTransaction()
        {
            _objTran = await Context.Database.BeginTransactionAsync();
        }

        public async Task Rollback()
        {
            await _objTran.RollbackAsync();
            await _objTran.DisposeAsync();
        }

        public async Task<int> Save()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while executing this operation");
            }
        }
    }
}
