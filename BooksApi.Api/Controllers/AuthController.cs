using BooksApi.Api.Models;
using BooksApi.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BooksApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (user, error) = await _authService.RegisterAsync(registerDto);

            if (user == null)
            {
                return BadRequest(new { Message = error });
            }

            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (token, error) = await _authService.LoginAsync(loginDto);
            if (token == null)
            {
                return Unauthorized(new { Message = error });
            }
            return Ok(new { Token = token });
        }
    }
}