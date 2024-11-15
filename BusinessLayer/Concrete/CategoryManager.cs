using Base.Utilities.Business;
using Base.Utilities.Results;
using BusinessLayer.Abstract;
using BusinessLayer.BusinessAspects.Autofac;
using BusinessLayer.Constants;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(Category entity)
        {
            var data = await _categoryDal.GetAsync(c => c.CategoryId == entity.CategoryId);
            if (data != null)
            {
                data.IsDeleted = true;
                await _categoryDal.UpdateAsync(data);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public async Task<IDataResult<Category>> GetByIdAsync(int id)
        {
            var data = await _categoryDal.GetAsync(c => c.CategoryId == id && c.IsDeleted == false);
            if (data != null)
            {
                return new SuccessDataResult<Category>(data);
            }
            return new ErrorDataResult<Category>();
        }

        public async Task<IDataResult<List<Category>>> GetAllAsync()
        {
            var result = await _categoryDal.GetWithIncludeAsync(
                include: q => q
                    .Include(p => p.Images)
                        .ThenInclude(i => i.Image) // BaseImage'ı da getir
            );

            return new SuccessDataResult<List<Category>>(result.Where(c => c.IsDeleted == false).ToList());
        }

        public async Task<IDataResult<List<CategoryDto>>> GetAllCategoryDtoAsync()
        {
            var result = await _categoryDal.GetAllCategoryDtoAsync();
            if (result != null)
            {
                return new SuccessDataResult<List<CategoryDto>>(result);
            }
            return new ErrorDataResult<List<CategoryDto>>(result);
        }

        public async Task<IDataResult<CategoryDto>> GetCategoryDtoAsync(int id)
        {
            var result = await _categoryDal.GetCategoryDtoByIdAsync(c => c.CategoryId == id);
            if (result != null)
            {
                return new SuccessDataResult<CategoryDto>(result);
            }
            return new SuccessDataResult<CategoryDto>();
        }

        [SecuredOperation("admin")]
        public async Task<IResult> AddAsync(Category entity)
        {
            var result = BusinessRule.Run(await CheckCountCategoryAsync());
            if (!result.IsSuccess)
            {
                return result;
            }
            await _categoryDal.AddAsync(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        public async Task<IResult> UpdateAsync(Category entity)
        {
            await _categoryDal.UpdateAsync(entity);
            return new SuccessResult();
        }

        private async Task<IResult> CheckCountCategoryAsync()
        {
            var count = await _categoryDal.GetAllAsync(c => c.IsDeleted == false);
            if (count.Count > 15)
            {
                return new ErrorResult(Messages.CategoryCountLimited);
            }
            return new SuccessResult();
        }
    }
}
