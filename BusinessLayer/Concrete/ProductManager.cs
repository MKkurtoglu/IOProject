using Base.Utilities.Results;
using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Base.Aspects.Autofac.Transaction;
using Base.Aspects.Autofac.Validation;
using Base.Utilities.Business;
using BusinessLayer.BusinessAspects.Autofac;
using BusinessLayer.Constants;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.Abstract;

public class ProductManager : IProductService
{
    IProductDal _productDal;
    private ICategoryService _categoryService;
    private IImageService _imageService;

    public ProductManager(IProductDal productDal, IImageService imageService)
    {
        _imageService = imageService;
        _productDal = productDal;
    }

    public async Task<IDataResult<Product>> GetByIdAsync(int id)
    {
        var product = await _productDal.GetAsync(p => p.ProductId == id);
        if (product != null)
        {
            return new SuccessDataResult<Product>(product, "İşlem başarılı bir şekilde gerçekleşmiştir.");
        }
        return new ErrorDataResult<Product>("Ürün bulunamadı.");
    }

    public async Task<IDataResult<List<Product>>> GetAllAsync()
    {
        if (DateTime.Now.Hour == 0)
        {
            return new ErrorDataResult<List<Product>>("Şu an bakım saatidir.");
        }
        var products = await _productDal.GetWithIncludeAsync(
            include: q => q
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Model)
                .Include(p => p.Images)
        );

        return new SuccessDataResult<List<Product>>(products.ToList(), "Ürünler listelenmiştir.");
    }

    public async Task<IDataResult<List<ProductDto>>> GetAllWithImageAsync()
    {
        var data = await _productDal.ProductsDTOAsync();
        if (data != null)
        {
            CheckPrimary(data);
            return new SuccessDataResult<List<ProductDto>>(data);
        }
        return new ErrorDataResult<List<ProductDto>>(data, "Data Null değer olabilir");
    }

    [SecuredOperation("admin,productAdd")]
    [ValidationAspect(typeof(ProductValidator))]
    public async Task<IResult> AddAsync(Product product)
    {
        var result =  BusinessRule.Run(
           await CheckOfProductCountByCategoryId(product.CategoryId),
          await CheckSameProductName(product.ProductName)
        );

        if (result != null)
        {
            return result;
        }

        await _productDal.AddAsync(product);
        return new SuccessResult(Messages.ProductAdded);
    }

    private async Task<IResult> CheckOfProductCountByCategoryId(int categoryId)
    {
        var count = await _productDal.GetAllAsync(p => p.CategoryId == categoryId);
        if (count.Count >= 10)
        {
            return new ErrorResult(Messages.ProductCategoryCountError);
        }
        return new SuccessResult();
    }

    private async Task<IResult> CheckSameProductName(string productName)
    {
        var exists = await _productDal.GetAsync(p => p.ProductName == productName);
        if (exists!=null)
        {
            return new ErrorResult(Messages.HaveSameProductName);
        }
        return new SuccessResult();
    }

    [TransactionScopeAspect]
    [ValidationAspect(typeof(ProductValidator))]
    public async Task<IResult> UpdateAsync(Product entity)
    {
        await _productDal.UpdateAsync(entity);

        if (entity.UnitPrice < 100)
        {
            throw new Exception();
        }
        entity.ProductName = entity.ProductName + "revize";
        await _productDal.UpdateAsync(entity);
        return new SuccessResult();
    }

    public async Task<IResult> DeleteAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IDataResult<List<ProductDto>>> GetAllByCategoryAsync(int id)
    {
        var result = await _productDal.ProductsDTOAsync(p => p.Category.CategoryId == id);
        CheckPrimary(result);
        return new SuccessDataResult<List<ProductDto>>(result);
    }

    private void CheckPrimary(List<ProductDto> products)
    {
        string defaultImagePath = @"C:\Users\kurto\source\repos\IOProject\WebAPI\wwwroot\images\logo.jpg";

        foreach (var product in products)
        {
            if (product.ImagePaths != null && product.ImagePaths.Any())
            {
                var primaryImage = product.PrimaryImagePath.FirstOrDefault();

                if (primaryImage == null)
                {
                    var firstImage = product.ImagePaths.FirstOrDefault();
                    if (firstImage == null)
                    {
                        product.ImagePaths = new List<string> { defaultImagePath };
                    }
                }
            }
            else
            {
                product.ImagePaths = new List<string> { defaultImagePath };
            }
        }
    }

    private void CheckPrimary(ProductDto product)
    {
        string defaultImagePath = @"C:\Users\kurto\source\repos\IOProject\WebAPI\wwwroot\images\logo.jpg";

        if (product.ImagePaths != null && product.ImagePaths.Any())
        {
            var primaryImage = product.PrimaryImagePath.FirstOrDefault();

            if (primaryImage == null)
            {
                var firstImage = product.ImagePaths.FirstOrDefault();
                if (firstImage == null)
                {
                    product.ImagePaths = new List<string> { defaultImagePath };
                }
            }
        }
        else
        {
            product.ImagePaths = new List<string> { defaultImagePath };
        }
    }

    public async Task<IDataResult<ProductDto>> GetProductDtoAsync(int id)
    {
        var result = await _productDal.ProductDTOAsync(p => p.Brand.IsDeleted == false && p.Category.IsDeleted == false && p.ProductId == id);
        if (result != null)
        {
            CheckPrimary(result);
            return new SuccessDataResult<ProductDto>(result);
        }
        return new ErrorDataResult<ProductDto>("Ürün bulunamadı.");
    }

   async Task <IDataResult<List<ProductDto>>> IProductService.GetAllDtoAsync()
    {
        var result = await _productDal.ProductsDTOAsync(p => p.Brand.IsDeleted == false && p.Category.IsDeleted == false );
        if (result != null)
        {
            CheckPrimary(result);
            return new SuccessDataResult<List<ProductDto>>(result);
        }
        return new ErrorDataResult<List<ProductDto>>("Ürün bulunamadı.");
    }
}
