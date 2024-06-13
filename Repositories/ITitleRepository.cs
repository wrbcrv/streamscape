using Api.Models;

namespace Api.Repositories
{
    public interface ITitleRepository
    {
        Task<IEnumerable<Title>> GetAllAsync();
        Task<Title> GetByIdAsync(int id);
        Task<Title> AddAsync(Title entity);
        Task<Title> UpdateAsync(Title entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Title>> SearchAsync(string query);
    }
}