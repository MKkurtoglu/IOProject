using Base.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class, new()
    {
        Task<IDataResult<List<T>>> GetAllAsync();
        Task<IDataResult<T>> GetByIdAsync(int id);
        Task<IResult> AddAsync(T entity);
        Task<IResult> UpdateAsync(T entity);
        Task<IResult> DeleteAsync(T entity);
    }
}
