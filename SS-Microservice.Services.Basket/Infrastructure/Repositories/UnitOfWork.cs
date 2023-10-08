﻿using Microsoft.EntityFrameworkCore.Storage;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Basket.Infrastructure.Data.DBContext;
using System.Collections;

namespace SS_Microservice.Services.Basket.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;

        public BasketDBContext Context { get; }

        public UnitOfWork(BasketDBContext context)
        {
            Context = context;
        }

        private bool _disposed;
        private string _errorMessage = string.Empty;
        private IDbContextTransaction _objTran;

        public IGenericRepository<T> Repository<T>() where T : class
        {
            _repositories ??= new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), Context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[type];
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

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Context.Dispose();
            _disposed = true;
        }
    }
}