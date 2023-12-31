﻿using SS_Microservice.Common.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(object id);

        Task<bool> Insert(T entity);

        bool Update(T entity);

        bool Delete(T entity);

        Task<T> GetEntityWithSpec(ISpecifications<T> specification);

        Task<List<T>> ListAsync(ISpecifications<T> specification);

        Task<int> CountAsync(ISpecifications<T> specifications);
    }
}