using System.Security.Claims;
using api.DTOs.Login;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UserManager<Usuario> _userManager;

        public LoginController(ITokenService tokenService, IUsuarioRepository usuarioRepository, UserManager<Usuario> userManager)
        {
            _tokenService = tokenService;
            _usuarioRepository = usuarioRepository;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginReqDTO request)
        {
            try
            {
                var usuario = await _usuarioRepository.FindByEmailAndSenhaAsync(request.Email, request.Senha);

                if (usuario == null)
                {
                    return Unauthorized(new { message = "Credenciais inválidas" });
                }

                var roles = await _userManager.GetRolesAsync(usuario);

                var token = _tokenService.CreateToken(usuario, roles);

                var options = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                };

                Response.Cookies.Append("token", token, options);

                return Ok(new { message = "Login bem-sucedido." });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("token")]
        [AllowAnonymous]
        public IActionResult GetToken()
        {
            var token = Request.Cookies["token"];

            if (string.IsNullOrEmpty(token))
            {
                return NotFound(new { message = "Token não encontrado." });
            }

            return Ok(new { token });
        }
    }
}