using Base.Utilities.Results;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;

namespace BusinessLayer.Abstract
{
    public interface ICartService : IGenericService<Cart>
    {
        Task<IDataResult<List<CartDto>>> GetCartItemsAsync();
        Task<IDataResult<decimal>> GetCartTotalAsync();
        Task<IResult> UpdateQuantityAsync(int cartId, int quantity);
    }
}
