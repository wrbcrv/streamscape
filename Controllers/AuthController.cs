using System.Security.Claims;
using Api.DTOs;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userService.GetByUsernameAndPassword(loginDTO.Username, loginDTO.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Username or password is incorrect" });
            }

            var token = _authService.Authenticate(user);

            return Ok(new { token });
        }

        [HttpGet("me")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<UserResponseDTO>> GetCurrentUser()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetByIdAsync(int.Parse(id));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}