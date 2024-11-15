using BusinessLayer.Abstract;
using Base.Utilities.Results;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.ValidationRules.FluentValidation;
using FluentValidation;
using Base.CrossCuttingConcerns.ValidationTools;
using Base.Aspects.Autofac.Validation;
using EntitiesLayer.Concrete;
using BusinessLayer.BusinessAspects.Autofac;

namespace BusinessLayer.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(Brand entity)
        {
            var data = await _brandDal.GetAsync(b => b.BrandId == entity.BrandId);
            if (data == null)
            {
                return new ErrorResult("Silmek istediğiniz marka silinememiştir.");
            }
            data.IsDeleted = true;
            await _brandDal.UpdateAsync(data);
            return new SuccessResult();
        }

        public async Task<IDataResult<Brand>> GetAsync(int id)
        {
            var result = await _brandDal.GetAsync(b => b.BrandId == id && b.IsDeleted == false);
            return new SuccessDataResult<Brand>(result);
        }

        public async Task<IDataResult<List<Brand>>> GetAllAsync()
        {
            var result = await _brandDal.GetAllAsync(b => b.IsDeleted == false);
            return new SuccessDataResult<List<Brand>>(result);
        }

        [ValidationAspect(typeof(BrandValidator))]
        [SecuredOperation("admin")]
        public async Task<IResult> AddAsync(Brand entity)
        {
            await _brandDal.AddAsync(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(BrandValidator))]
        public async Task<IResult> UpdateAsync(Brand entity)
        {
            await _brandDal.UpdateAsync(entity);
            return new SuccessResult();
        }

        public async Task<IDataResult<Brand>> GetByIdAsync(int id)
        {
            var result = await _brandDal.GetAsync(b => b.BrandId == id && b.IsDeleted == false);
            return new SuccessDataResult<Brand>(result);
        }
      
    }
}
