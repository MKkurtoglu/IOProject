using Base.EntitiesBase.Concrete;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getProfileUser")]
        public async Task<IActionResult> GetProfileUserAsync()
        {
            var result = await _userService.GetDtoAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUserAsync(User user)
        {
            var result = await _userService.AddAsync(user);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("deleteUser")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var userResult = await _userService.GetByIdAsync(id);
            if (!userResult.IsSuccess)
                return BadRequest(userResult);

            var deleteResult = await _userService.DeleteAsync(userResult.Data);
            return deleteResult.IsSuccess ? Ok(deleteResult) : BadRequest(deleteResult);
        }

        [HttpPost("updateUser")]
        public async Task<IActionResult> UpdateUserAsync(User user)
        {
            var result = await _userService.UpdateAsync(user);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
