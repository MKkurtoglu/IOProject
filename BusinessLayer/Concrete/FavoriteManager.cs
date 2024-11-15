using Base.Extensions;
using Base.Utilities.Results;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class FavoriteManager : IFavoriteService
    {
        IFavoriteDal _favoriteDal;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FavoriteManager(IFavoriteDal favoriteDal, IHttpContextAccessor httpContextAccessor)
        {
            _favoriteDal = favoriteDal;
            _httpContextAccessor = httpContextAccessor;
        }

        // Asynchronous Delete
        public async Task<IResult> DeleteAsync(Favorites entity)
        {
            if (entity != null)
            {
                await _favoriteDal.DeleteAsync(entity);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        // Asynchronous Get by Id
        public async Task<IDataResult<Favorites>> GetByIdAsync(int id)
        {
            var result = await _favoriteDal.GetAsync(f => f.FavoriteId == id);
            if (result == null)
            {
                return new ErrorDataResult<Favorites>("Favorite not found.");
            }
            return new SuccessDataResult<Favorites>(result);
        }

        // Asynchronous Get All
        public async Task<IDataResult<List<Favorites>>> GetAllAsync()
        {
            var id = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(id);
            var result = await _favoriteDal.GetAllAsync(f => f.UserId == userId);

            if (result != null)
            {
                return new SuccessDataResult<List<Favorites>>(result);
            }
            return new ErrorDataResult<List<Favorites>>("No favorites found.");
        }

        // Asynchronous Insert
        public async Task<IResult> AddAsync(Favorites entity)
        {
            if (entity != null)
            {
                await _favoriteDal.AddAsync(entity);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        // Asynchronous Update
        public async Task<IResult> UpdateAsync(Favorites entity)
        {
            if (entity != null)
            {
                await _favoriteDal.UpdateAsync(entity);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        // Asynchronous FavoriteDtos
        public async Task<IDataResult<List<FavoriteDto>>> FavoriteDtosAsync()
        {
            var id = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(id);
            var data = await _favoriteDal.GetAllFavoriteByUserAsync(userId);

            return data == null
                ? new ErrorDataResult<List<FavoriteDto>>(new List<FavoriteDto>(), "No favorites found.")
                : new SuccessDataResult<List<FavoriteDto>>(data);
        }
    }
}
