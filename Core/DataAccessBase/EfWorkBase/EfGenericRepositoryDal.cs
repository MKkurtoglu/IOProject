using Base.EntitiesBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Base.DataAccessBase.EfWorkBase
{
    public class EfGenericRepositoryDal<T, Context> : IGenericDal<T>
        where T : class, IEntity, new()
        where Context : DbContext, new()
    {
        public async Task AddAsync(T entity)
        {
            using (var context = new Context())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            using (var context = new Context())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            using (var context = new Context())
            {
                return await context.Set<T>().FirstOrDefaultAsync(filter);
            }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            using (var context = new Context())
            {
                return filter == null
                    ? await context.Set<T>().ToListAsync()
                    : await context.Set<T>().Where(filter).ToListAsync();
            }
        }

        public async Task<IQueryable<T>> GetWithIncludeAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
        )
        {
            var context = new Context();
            IQueryable<T> query = context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            return await Task.FromResult(query);
        }

        public async Task UpdateAsync(T entity)
        {
            using (var context = new Context())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
