using Api.DTOs;

namespace Api.Services
{
    public interface ITitleService
    {
        Task<IEnumerable<TitleResponseDTO>> GetAllAsync();
        Task<TitleResponseDTO> GetByIdAsync(int id);
        Task<TitleResponseDTO> AddAsync(TitleDTO titleDto);
        Task<TitleResponseDTO> UpdateAsync(int id, TitleDTO titleDto);
        Task<bool> DeleteAsync(int id);
        Task<EpisodeResponseDTO> AddEpisodeAsync(int titleId, EpisodeDTO episodeDTO);
    }
}