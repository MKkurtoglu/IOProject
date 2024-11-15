using Base.DataAccessBase;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICartDal : IGenericDal<Cart>
    {
        Task<List<CartDto>> GetAllCartItemsByUserAsync(int userId);
        Task<decimal> GetCartTotalAsync(int userId);
    }
}
