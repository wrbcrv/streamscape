using api.DTOs.Titulo;
using api.Interfaces;
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

        public TituloController(ITituloRepository tituloRepository, IFileRepository fileRepository)
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

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var titulo = await _tituloRepository.GetByIdAsync(id);

                if (titulo == null)
                {
                    return NotFound(new { message = "Título não encontrado." });
                }

                return Ok(titulo);
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

        [HttpPost]
        [Route("{id}/thumb/upload")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadImage([FromRoute] int id, [FromForm] FileUploadModel model)
        {
            try
            {
                var titulo = await _tituloRepository.GetByIdAsync(id);

                if (titulo == null)
                {
                    return NotFound(new { message = "Título não encontrado." });
                }

                var imagePath = await _tituloRepository.AddImageAsync(id, model.File);

                return Ok(new { message = "Imagem adicionada com sucesso.", imagePath });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}/thumb/download")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadImage([FromRoute] int id)
        {
            try
            {
                byte[] data = await _tituloRepository.DownloadImageAsync(id);

                return File(data, "image/jpeg");
            }
            catch (ArgumentException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}