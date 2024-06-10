using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Api.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/titles")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly ITitleService _titleService;

        public TitlesController(ITitleService titleService)
        {
            _titleService = titleService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<TitleResponseDTO>>> GetAll()
        {
            try
            {
                var titles = await _titleService.GetAllAsync();
                return Ok(titles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<TitleResponseDTO>> GetById(int id)
        {
            try
            {
                var title = await _titleService.GetByIdAsync(id);

                if (title == null)
                {
                    return NotFound();
                }

                return Ok(title);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TitleResponseDTO>> Add(TitleDTO titleDTO)
        {
            try
            {
                var title = await _titleService.AddAsync(titleDTO);
                return CreatedAtAction(nameof(GetById), new { id = title.Id }, title);
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

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TitleResponseDTO>> Update(int id, TitleDTO titleDTO)
        {
            try
            {
                var title = await _titleService.UpdateAsync(id, titleDTO);
                return Ok(title);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _titleService.DeleteAsync(id);

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

        [HttpPost("{title_id}/episodes")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEpisode(int title_id, [FromForm] UploadDTO episodeDTO)
        {
            try
            {
                var episode = await _titleService.AddEpisodeAsync(title_id, episodeDTO);
                return Ok(episode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{title_id}/episodes/{episode_id}/download")]
        public async Task<IActionResult> DownloadEpisode(int title_id, int episode_id)
        {
            try
            {
                var result = await _titleService.DownloadEpisodeAsync(title_id, episode_id);
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}