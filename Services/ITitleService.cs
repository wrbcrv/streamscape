using Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services
{
    public interface ITitleService
    {
        Task<IEnumerable<TitleResponseDTO>> GetAllAsync();
        Task<TitleResponseDTO> GetByIdAsync(int id);
        Task<TitleResponseDTO> AddAsync(TitleDTO titleDto);
        Task<TitleResponseDTO> UpdateAsync(int id, TitleDTO titleDto);
        Task<bool> DeleteAsync(int id);
        Task<EpisodeResponseDTO> AddEpisodeAsync(int titleId, UploadDTO episodeDTO);
        Task<FileStreamResult> DownloadEpisodeAsync(int titleId, int episodeId);
        Task<FileStreamResult> DownloadThumbnailAsync(int titleId);
    }
}