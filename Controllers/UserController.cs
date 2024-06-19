using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace UserApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> Add(UserDTO userDTO)
        {
            try
            {
                var user = await _userService.AddAsync(userDTO);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<UserDTO>> Update(int id, UserUpdateDTO userDTO)
        {
            try
            {
                var user = await _userService.UpdateAsync(id, userDTO);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin , User")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _userService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{uid}/titles/{tid}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<UserResponseDTO>> AddToMyList(int uid, int tid)
        {
            try
            {
                var (user, message) = await _userService.AddToMyList(uid, tid);
                return Ok(new { Message = message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{uid}/titles/{tid}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult> RemoveFromMyList(int uid, int tid)
        {
            try
            {
                var (user, message) = await _userService.RemoveFromMyList(uid, tid);
                return Ok(new { Message = message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}