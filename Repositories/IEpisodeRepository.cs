using Api.Models;

namespace Api.Repositories
{
    public interface IEpisodeRepository
    {
        Task<IEnumerable<Episode>> GetAllAsync();
        Task<Episode> GetByIdAsync(int id);
        Task<Episode> AddAsync(Episode entity);
        Task<Episode> UpdateAsync(Episode entity);
        Task<bool> DeleteAsync(int id);
    }
}