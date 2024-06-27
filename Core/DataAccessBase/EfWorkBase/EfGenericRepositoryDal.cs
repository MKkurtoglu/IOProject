using Base.EntitiesBase;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataAccessBase.EfWorkBase
{
    public class EfGenericRepositoryDal<T, Context> : IGenericDal<T> where T : class, IEntity, new() where Context : DbContext, new()
    {
        
        
        public void Add(T entity)
        {
            
            using (var context = new Context())
            {
                var addedEntity= context.Entry(entity); // burada gelecek nesneyi bd de referansını ayarladık.
                addedEntity.State= EntityState.Added;
                context.SaveChanges(); // değişiklikleri kaydet
                
            }
        }

        public void Delete(T entity)
        {
            using (var context = new Context())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State= EntityState.Deleted;   
                context.SaveChanges();
            }
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            using (var context = new Context())
            {
                return context.Set<T>().FirstOrDefault(filter);
            }
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            using (var context = new Context())
            {
                return filter == null ? context.Set<T>().ToList() : context.Set<T>().Where(filter).ToList();
            }
        }

        public void Update(T entity)
        {
            using (var context = new Context())
            {
                var uptededEntity = context.Entry(entity);
                uptededEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
