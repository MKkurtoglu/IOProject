using Base.Utilities.Results;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IFavoriteService : IGenericService<Favorites>
    {
        Task<IDataResult<List<FavoriteDto>>> FavoriteDtosAsync();
    }
}
