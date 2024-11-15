
using Base.DataAccessBase;
using Base.EntitiesBase.Concrete;
using Base.Utilities.Results;
using EntitiesLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        Task<IDataResult<List<OperationClaim>>> GetClaimsAsync(User user);
        Task<IDataResult<User>> GetByMailAsync(string email);
        Task<IDataResult<User>> GetByIdAsync();
        Task<IDataResult<UserProfileDto>> GetDtoAsync();
    }

}
