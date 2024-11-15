using Base.Utilities.Results;
using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System.Threading.Tasks;

public interface IProductService : IGenericService<Product>
{
    Task<IDataResult<List<ProductDto>>> GetAllByCategoryAsync(int id);
    Task<IDataResult<List<ProductDto>>> GetAllWithImageAsync();
    Task<IDataResult<List<ProductDto>>> GetAllDtoAsync();
    Task<IDataResult<ProductDto>> GetProductDtoAsync(int id);
}
