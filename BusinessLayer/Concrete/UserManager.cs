using BusinessLayer.Abstract;
using Base.Utilities.Results;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Aspects.Autofac.Validation;
using BusinessLayer.ValidationRules.FluentValidation;
using Base.EntitiesBase.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Base.Aspects.Autofac.Cache;
using Base.Utilities.Security.JWT;
using Microsoft.AspNetCore.Http;
using BusinessLayer.BusinessAspects.Autofac;
using EntitiesLayer.DTOs;
using Base.Extensions;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManager(IUserDal userDal, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor)
        {
            _userDal = userDal;
            _tokenHelper = tokenHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResult> DeleteAsync(User entity)
        {
            await _userDal.DeleteAsync(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin,customer")]
        public async Task<IDataResult<User>> GetByIdAsync(int id)
        {
            var userId = int.Parse(_tokenHelper.GetId());
            var result = await _userDal.GetAsync(u => u.Id == userId);
            return new SuccessDataResult<User>(result);
        }

        [CacheAspect]
        public async Task<IDataResult<List<User>>> GetAllAsync()
        {
            var result = await _userDal.GetAllAsync();
            return new SuccessDataResult<List<User>>(result.ToList());
        }

        [SecuredOperation("admin,customer")]
        public async Task<IDataResult<User>> GetByIdAsync()
        {
            var userId = int.Parse(_tokenHelper.GetId());
            var result = await _userDal.GetAsync(u => u.Id == userId);
            return new SuccessDataResult<User>(result);
        }

        public async Task<IDataResult<User>> GetByMailAsync(string email)
        {
            var result = await _userDal.GetAsync(u => u.Email == email);
            return result == null
                ? new ErrorDataResult<User>(result)
                : new SuccessDataResult<User>(result);
        }

        public async Task<IDataResult<List<OperationClaim>>> GetClaimsAsync(User user)
        {
            var result = await _userDal.GetClaimsAsync(user);
            return new SuccessDataResult<List<OperationClaim>>(result);
        }

        [CacheRemoveAspect("IUserService.get")]
        public async Task<IResult> AddAsync(User entity)
        {
            await _userDal.AddAsync(entity);
            return new SuccessResult();
        }

        public async Task<IResult> UpdateAsync(User entity)
        {
            await _userDal.UpdateAsync(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin,customer")]
        public async Task<IDataResult<UserProfileDto>> GetDtoAsync()
        {
            var id = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(id);
            var result = await _userDal.GetProfileDtoAsync(userId);

            return result == null
                ? new ErrorDataResult<UserProfileDto>("Kullanıcı Bilgileri Bulunamadı")
                : new SuccessDataResult<UserProfileDto>(result, "Kullanıcı Bilgileri Alındı");
        }
    }
}
