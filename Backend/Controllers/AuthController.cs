using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthInterface _authInterface;

        public AuthController(IAuthInterface authInterface)
        {
            _authInterface = authInterface;
        }

        [HttpPost("registerUser")]
        public async Task<ActionResult<UserDTO>> RegisterUser(UserDTO dto)
        {
            var register = await _authInterface.RegisterUser(dto);
            return Ok(register);
        }

        [HttpPost("loginUser")]
        public async Task<ActionResult<UserDTO>> AuthUser(UserDTO dto)
        {
            var login = await _authInterface.AuthUser(dto);
            return Ok(login);
        }
    }
}
