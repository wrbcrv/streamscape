using Api.Models;
using Microsoft.EntityFrameworkCore;
using Api.Data;

namespace Api.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<Genre> GetByNameAsync(Models.Type name)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Type == name);
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
    }
}
