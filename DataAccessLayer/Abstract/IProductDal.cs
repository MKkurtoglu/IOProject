using Base.DataAccessBase;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        Task<List<ProductDto>> ProductsDTOAsync(Expression<Func<ProductDto, bool>> filter = null);
        Task<ProductDto> ProductDTOAsync(Expression<Func<ProductDto, bool>> filter = null);
    }
}
