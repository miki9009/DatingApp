using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthRepository _repository;
        public AuthController(IAuthRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {

            userRegisterDto.Username = userRegisterDto.Username.ToLower();

            if(await _repository.UserExists(userRegisterDto.Username))
            return BadRequest("Username already exists");

            var userToCreate = new User
            {
                UserName = userRegisterDto.Username
            };

            var createdUser = await _repository.Register(userToCreate, userRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return StatusCode(201);
        }

    }
}