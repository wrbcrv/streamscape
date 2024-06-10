using Api.Models;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;

namespace Api.Repositories
{
    public class TitleRepository(AppDbContext context) : ITitleRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Title>> GetAllAsync()
        {
            return await _context.Titles.Include(t => t.TitleGenres).ThenInclude(tg => tg.Genre).Include(t => t.Episodes).ToListAsync();
        }

        public async Task<Title> GetByIdAsync(int id)
        {
            return await _context.Titles.Include(t => t.TitleGenres).ThenInclude(tg => tg.Genre).Include(t => t.Episodes).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Title> AddAsync(Title title)
        {
            await _context.Titles.AddAsync(title);
            await _context.SaveChangesAsync();
            return title;
        }

        public async Task<Title> UpdateAsync(Title title)
        {
            _context.Titles.Update(title);
            await _context.SaveChangesAsync();
            return title;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var title = await _context.Titles.FindAsync(id);

            if (title == null)
            {
                return false;
            }

            _context.Titles.Remove(title);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
