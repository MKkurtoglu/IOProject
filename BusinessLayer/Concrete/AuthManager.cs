using Azure.Core;
using Base.EntitiesBase.Concrete;
using Base.Utilities.Business;
using Base.Utilities.Results;
using Base.Utilities.Security.Hashing;
using Base.Utilities.Security.JWT;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using EntitiesLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public async Task<IDataResult<User>> RegisterAsync(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            await _userService.AddAsync(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public async Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByMailAsync(userForLoginDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        public async Task<IResult> UserExistsAsync(string email)
        {
            var userExists = await _userService.GetByMailAsync(email);
            if (userExists.IsSuccess)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public async Task<IDataResult<Base.Utilities.Security.JWT.AccessToken>> CreateAccessTokenAsync(User user)
        {
            var claims = await _userService.GetClaimsAsync(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<Base.Utilities.Security.JWT.AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public async Task<IResult> UpdateUserAsync(UserForUpdateDto userForUpdateDto)
        {
            IResult checkNullResult = CheckNull(userForUpdateDto);
            IResult[] methods = new[] { checkNullResult };

            var resultRun = BusinessRule.Run(methods);
            if (resultRun.IsSuccess)
            {
                var userResult = await _userService.GetByIdAsync();
                if (userResult.Data != null)
                {
                    var user = userResult.Data;
                    user.FirstName = userForUpdateDto.FirstName;
                    user.LastName = userForUpdateDto.LastName;
                    user.Email = userForUpdateDto.Email;

                    var result = await _userService.UpdateAsync(user);
                    if (result.IsSuccess)
                    {
                        return new SuccessResult("Kullanıcı Güncellendi");
                    }
                    return new ErrorResult("Kullanıcı Güncellenme İşlemi Başarısız");
                }
                return new ErrorResult("Kullanıcı Güncellenme İşlemi Başarısız");
            }
            return resultRun;
        }

        // gelen verinin null olup olmadığını kontrol etme

        private IResult CheckNull<T>(T value)
        {
            if (value != null)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }
    }
}
