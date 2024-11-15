using Base.DataAccessBase;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IFavoriteDal : IGenericDal<Favorites>
    {
        Task<List<FavoriteDto>> GetAllFavoriteByUserAsync(int userId);
    }
}
