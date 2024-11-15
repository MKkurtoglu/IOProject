using Base.DataAccessBase;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        Task<CategoryDto> GetCategoryDtoByIdAsync(Expression<Func<CategoryDto, bool>> filter = null);
        Task<List<CategoryDto>> GetAllCategoryDtoAsync(Expression<Func<CategoryDto, bool>> filter = null);
    }
}
