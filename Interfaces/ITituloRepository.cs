using api.DTOs.Titulo;
using api.Models;

namespace api.Repository
{
    public interface ITituloRepository
    {
        Task<List<Titulo>> GetAllAsync();
        Task<Titulo?> GetByIdAsync(int id);
        Task<Titulo> CreateAsync(TituloReqDTO request);
        Task<Titulo> AddImageAsync(int tituloId, IFormFile image);
        Task<byte[]> DownloadImageAsync(int tituloId);
    }
}