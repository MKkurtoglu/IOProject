using Base.DataAccessBase;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IModelDal : IGenericDal<Model>
    {
        Task<List<ModelDto>> GetModelsDtoAsync(Expression<Func<ModelDto, bool>> filter = null);
    }
}
