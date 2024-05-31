using Api.Models;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> GetByIdAsync(int id);
        Task<Genre> GetByNameAsync(string name);
        Task<Genre> AddAsync(Genre genre);
    }
}