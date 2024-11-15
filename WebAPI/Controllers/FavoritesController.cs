using Base.Extensions;
using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using EntitiesLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FavoritesController(IFavoriteService favoriteService, IHttpContextAccessor httpContextAccessor)
        {
            _favoriteService = favoriteService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("getAllFavorites")]
        public async Task<IActionResult> GetAllFavoritesAsync()
        {
            var result = await _favoriteService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getFavoriteById")]
        public async Task<IActionResult> GetFavoriteByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Geçersiz ID" });

            var result = await _favoriteService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("addFavorite")]
        public async Task<IActionResult> AddFavoriteAsync(FavoriteAddDto favoriteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = _httpContextAccessor.HttpContext.User.FindId();
            var userId = int.Parse(id);

            var favorite = new Favorites
            {
                ProductId = favoriteDto.ProductId,
                UserId = userId
            };

            var result = await _favoriteService.AddAsync(favorite);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("deleteFavorite")]
        public async Task<IActionResult> DeleteFavoriteAsync(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Geçersiz ID" });

            var favoriteToDelete = await _favoriteService.GetByIdAsync(id);
            if (!favoriteToDelete.IsSuccess)
                return NotFound(new { message = "Favori bulunamadı" });

            var result = await _favoriteService.DeleteAsync(favoriteToDelete.Data);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        /*
        [HttpPut("updateFavorite")]
        public async Task<IActionResult> UpdateFavoriteAsync(FavoriteUpdateDto favoriteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingFavorite = await _favoriteService.GetByIdAsync(favoriteDto.FavoriteId);
            if (!existingFavorite.IsSuccess)
                return NotFound(new { message = "Güncellenecek favori bulunamadı" });

            var favorite = existingFavorite.Data;
            favorite.ProductId = favoriteDto.ProductId;
            favorite.UserId = favoriteDto.UserId;

            var result = await _favoriteService.UpdateAsync(favorite);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        */

        [HttpGet("getAllFavoritesByUser")]
        public async Task<IActionResult> GetUserFavoritesAsync()
        {
            var result = await _favoriteService.FavoriteDtosAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}

// DTO'lar için örnek sınıflar (EntitiesLayer.DTOs namespace'i altında oluşturulmalı)
