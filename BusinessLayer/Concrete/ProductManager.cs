using Base.Aspects.Autofac.Validation;
using Base.CrossCuttingConcerns.ValidationTools;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using Base.Utilities.Results;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using System.Linq.Expressions;

using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Base.Utilities.Business;
using BusinessLayer.BusinessAspects.Autofac;
using Base.Aspects.Autofac.Cache;
using Base.Aspects.Autofac.Transaction;

namespace BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
          IProductDal _productDal;
        private   ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }
        public IDataResult<Product> Get(int id)
        {
            var result = _productDal.Get(p => p.ProductId == id);
            if (result != null)
            {
                return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id), "İşlem başarılı bir şekilde gerçekleşmiştir.");
            }
            else
            {
                return new ErrorDataResult<Product>("ürün gönderimi başarısızdır.");
            }

        }
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 0)
            {
                return new ErrorDataResult<List<Product>>("Şu an bakım saatidir.");
                // default olarak list in defaultu null ve null dönecektir.
                //
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), "Ürünler listelenmiştir.");
            //burada istersek mesajlar için sabit mesajalr oluşturabiliriz.
        }




        //[SecuredOperation("admin,productAdd")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Insert(Product product)
        {

            var result = BusinessRule.Run(CheckOfProductCountByCategoryId(product.CategoryId), CheckSameProductName(product.ProductName), CheckAddByCategoryCount());


            if (result != null)
            {
                return result;
            }


            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }


        private IResult CheckOfProductCountByCategoryId(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId);
            if (result.Count >= 10)
            {
                return new ErrorResult(Messages.ProductCategoryCountError);
            }
            return new SuccessResult();
        }

        private IResult CheckSameProductName(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.HaveSameProductName);
            }
            return new SuccessResult();
        }
        private IResult CheckAddByCategoryCount()
        {

            {
                var result = _categoryService.GetAll().Data.Count;
                if (result > 15)
                {
                    return new ErrorResult(Messages.CategoryCountLimited);
                }
                return new SuccessResult();
            }
        }
        [TransactionScopeAspect]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product entity)
        {
            _productDal.Update(entity);
            if (entity.UnitPrice < 100) {
                throw new Exception();
            }
            entity.ProductName = entity.ProductName + "revize";
            _productDal.Update(entity);
            return new SuccessResult();
        }

        public IResult Delete(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
