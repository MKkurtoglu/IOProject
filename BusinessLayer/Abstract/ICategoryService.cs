using Base.Utilities.Results;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<IDataResult<List<CategoryDto>>> GetAllCategoryDtoAsync();
        Task<IDataResult<CategoryDto>> GetCategoryDtoAsync(int id);
    }
}
