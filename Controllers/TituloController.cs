using api.DTOs.Titulo;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/titulos")]
    [ApiController]
    public class TituloController : ControllerBase
    {
        private readonly ITituloRepository _tituloRepository;

        public TituloController(ITituloRepository tituloRepository)
        {
            _tituloRepository = tituloRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var titulos = await _tituloRepository.GetAllAsync();

                var response = titulos.Select(TituloResDTO.valueOf).ToList();

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TituloReqDTO request)
        {
            try
            {
                var titulo = await _tituloRepository.CreateAsync(request);

                return Ok(titulo);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}