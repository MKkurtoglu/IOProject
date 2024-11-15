using Azure.Core;
using Base.EntitiesBase.Concrete;
using Base.Utilities.Results;
using EntitiesLayer.DTOs;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> RegisterAsync(UserForRegisterDto userForRegisterDto, string password);
        Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto);
        Task<IResult> UserExistsAsync(string email);
        Task<IDataResult<Base.Utilities.Security.JWT.AccessToken>> CreateAccessTokenAsync(User user);
        Task<IResult> UpdateUserAsync(UserForUpdateDto userForUpdateDto);
    }
}
