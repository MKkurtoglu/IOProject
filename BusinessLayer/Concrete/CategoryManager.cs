using Base.Utilities.Business;
using Base.Utilities.Results;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using EntitiesLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryService _categoryService;
        public CategoryManager(ICategoryService categoryService)
        {

            _categoryService = categoryService;

        }

        public IResult Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Category> Get(int id)
        {
            _categoryService.Get(id);
            return new SuccessDataResult<Category>();
        }

        public IDataResult<List<Category>> GetAll()
        {
            _categoryService.GetAll();
            return new SuccessDataResult<List<Category>>();
        }

        public IResult Insert(Category entity)
        {
            var result=BusinessRule.Run(CheckCountCategory());
            if (!result.IsSuccess)
            {
                return result;
            }
            _categoryService.Insert(entity);
            return new SuccessResult();

        }

        public IResult Update(Category entity)
        {
            throw new NotImplementedException();
        }

        private IResult CheckCountCategory()
        {
            var result =_categoryService.GetAll().Data.Count;
            if (result>15)
            {
                return new ErrorResult(Messages.CategoryCountLimited);
            }
            return new SuccessResult();
        }
    }
}
