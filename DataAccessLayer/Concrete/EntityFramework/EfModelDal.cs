using Base.DataAccessBase.EfWorkBase;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfModelDal : EfGenericRepositoryDal<Model, ProjectDbContext>, IModelDal
    {
        public async Task<List<ModelDto>> GetModelsDtoAsync(Expression<Func<ModelDto, bool>> filter = null)
        {
            using (var context = new ProjectDbContext())
            {
                var query = context.Models
                    .Include(m => m.Brand)
                    .Select(m => new ModelDto
                    {
                        ModelId = m.ModelId,
                        ModelName = m.ModelName,
                        BrandId = m.BrandId,
                        BrandName = m.Brand.BrandName,
                        IsDeleted = m.Brand.IsDeleted
                    });

                // Eğer filtre sağlanmışsa, filtreyi uygula
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return await query.ToListAsync();
            }
        }
    }
}
