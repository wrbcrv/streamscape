using api.DTOs.Titulo;
using api.Models;

namespace api.Repository
{
    public interface ITituloRepository
    {
        Task<List<Titulo>> GetAllAsync();
        Task<Titulo> CreateAsync(TituloReqDTO request);
    }
}