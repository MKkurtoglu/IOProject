using Base.Utilities.Results;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ModelManager : IModelService
    {
        IModelDal _modelDal;

        public ModelManager(IModelDal modelDal)
        {
            _modelDal = modelDal;
        }

        public async Task<IResult> DeleteAsync(Model entity)
        {
            await _modelDal.DeleteAsync(entity);
            return new SuccessResult();
        }

        public async Task<IDataResult<Model>> GetByIdAsync(int id)
        {
            var result = await _modelDal.GetAsync(m => m.ModelId == id && m.Brand.IsDeleted == false);
            if (result == null)
            {
                return new ErrorDataResult<Model>(result);
            }
            return new SuccessDataResult<Model>(result);
        }

        public async Task<IDataResult<List<ModelDto>>> GetAllModelWithBrandAsync()
        {
            var result = await _modelDal.GetModelsDtoAsync(m => m.IsDeleted == false);
            if (result == null)
            {
                return new ErrorDataResult<List<ModelDto>>(result);
            }
            return new SuccessDataResult<List<ModelDto>>(result);
        }

        public async Task<IDataResult<List<Model>>> GetAllAsync()
        {
            var result = await _modelDal.GetAllAsync(m => m.Brand.IsDeleted == false);
            if (result == null)
            {
                return new ErrorDataResult<List<Model>>(result);
            }
            return new SuccessDataResult<List<Model>>(result);
        }

        public async Task<IDataResult<List<Model>>> GetModelByBrandAsync(int brandId)
        {
            var result = await _modelDal.GetAllAsync(m => m.BrandId == brandId && m.Brand.IsDeleted == false);
            if (result == null)
            {
                return new ErrorDataResult<List<Model>>(result);
            }
            return new SuccessDataResult<List<Model>>(result);
        }

        public async Task<IResult> AddAsync(Model entity)
        {
            await _modelDal.AddAsync(entity);
            return new SuccessResult();
        }

        public async Task<IResult> UpdateAsync(Model entity)
        {
            await _modelDal.UpdateAsync(entity);
            return new SuccessResult();
        }
    }
}
