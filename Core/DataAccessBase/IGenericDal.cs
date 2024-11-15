﻿
using Base.EntitiesBase;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataAccessBase
{
    public interface IGenericDal<T> where T : class, IEntity, new()
    {
       
            Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
            Task<T> GetAsync(Expression<Func<T, bool>> filter);
            Task AddAsync(T entity);
            Task DeleteAsync(T entity);
            Task UpdateAsync(T entity);
            Task<IQueryable<T>> GetWithIncludeAsync(
                Expression<Func<T, bool>> filter = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            );
        
    }
}
