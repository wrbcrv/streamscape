using System.Security.Claims;
using api.DTOs.Login;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUsuarioRepository _usuarioRepository;
        public LoginController(ITokenService tokenService, IUsuarioRepository usuarioRepository)
        {
            _tokenService = tokenService;
            _usuarioRepository = usuarioRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginReqDTO request)
        {
            try
            {
                var usuario = await _usuarioRepository.FindByEmailAndSenhaAsync(request.Email, request.Senha);

                if (usuario == null)
                {
                    return Unauthorized(new { message = "Credenciais inválidas" });
                }

                var token = _tokenService.CreateToken(usuario);

                var options = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                };

                Response.Cookies.Append("token", token, options);

                return Ok(new { token });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}