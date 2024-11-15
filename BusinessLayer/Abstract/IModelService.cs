using Base.Utilities.Results;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IModelService : IGenericService<Model>
    {
        Task<IDataResult<List<Model>>> GetModelByBrandAsync(int brandId);
        Task<IDataResult<List<ModelDto>>> GetAllModelWithBrandAsync();
    }
}
