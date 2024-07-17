using Base.EntitiesBase.Concrete;
using BusinessLayer.Abstract;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("get")]
        public IActionResult Get(int id)
        {
            var result = _userService.Get(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("addUser")]
        public IActionResult AddUser(User user)
        {
            var result = _userService.Insert(user);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("deleteUser")]
        public IActionResult DeleteUser(int id)
        {
            var result = _userService.Get(id);
            var result2 = _userService.Delete(result.Data);
            if(result2.IsSuccess)
            {
                return Ok(result2);
            }
            return BadRequest();
        }
        [HttpPost("updateUser")]
        public IActionResult UpdateUser(User user)
        {
            var result = _userService.Update(user);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
