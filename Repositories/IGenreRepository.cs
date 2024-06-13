using Api.Models;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> GetByIdAsync(int id);
        Task<Genre> GetByNameAsync(Models.Type type);
        Task<Genre> AddAsync(Genre genre);
    }
}