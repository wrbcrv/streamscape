using api.DTOs.Usuario;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _usuarioRepository.GetAllAsync();

                var usuariosDTO = users.Select(UsuarioResDTO.ValueOf).ToList();

                return Ok(usuariosDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);

                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário não encontrado" });
                }

                return Ok(usuario);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UsuarioReqDTO request)
        {
            try
            {
                var usuario = await _usuarioRepository.CreateAsync(request);

                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário não encontrado" });
                }

                return Ok(usuario);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UsuarioReqDTO request)
        {
            try
            {
                var usuario = await _usuarioRepository.UpdateAsync(id, request);

                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário não encontrado" });
                }

                return Ok(usuario);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var usuario = await _usuarioRepository.DeleteAsync(id);

                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário não encontrado" });
                }

                return Ok(new { message = "Usuário excluído com sucesso" });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}