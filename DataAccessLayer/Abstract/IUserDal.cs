using Base.DataAccessBase;
using Base.EntitiesBase.Concrete;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUserDal : IGenericDal<User>
    {
        Task<List<OperationClaim>> GetClaimsAsync(User user);
        Task<UserProfileDto> GetProfileDtoAsync(int userId);
    }
}
