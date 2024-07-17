using Azure.Core;
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
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<Base.Utilities.Security.JWT.AccessToken> CreateAccessToken(User user);
    }
}
