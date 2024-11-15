using BusinessLayer.Abstract;
using EntitiesLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.LoginAsync(userForLoginDto);
            if (!userToLogin.IsSuccess)
                return BadRequest(new { message = userToLogin.Message });

            var result = await _authService.CreateAccessTokenAsync(userToLogin.Data);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserForRegisterDto userForRegisterDto)
        {
            // Email kontrolü
            var userExists = await _authService.UserExistsAsync(userForRegisterDto.Email);
            if (!userExists.IsSuccess)
                return BadRequest(new { message = userExists.Message });

            // Kullanıcı kaydı
            var registerResult = await _authService.RegisterAsync(
                userForRegisterDto,
                userForRegisterDto.Password
            );

            if (!registerResult.IsSuccess)
                return BadRequest(new { message = registerResult.Message });

            // Token oluşturma
            var tokenResult = await _authService.CreateAccessTokenAsync(registerResult.Data);
            return tokenResult.IsSuccess ? Ok(tokenResult) : BadRequest(tokenResult);
        }

        [HttpPost("updateUser")]
        public async Task<IActionResult> UpdateUserAsync(UserForUpdateDto userForUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.UpdateUserAsync(userForUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
