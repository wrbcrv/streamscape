using Api.Models;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;

namespace Api.Repositories
{
    public class EpisodeRepository(AppDbContext context) : IEpisodeRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Episode>> GetAllAsync()
        {
            return await _context.Episodes.ToListAsync();
        }

        public async Task<Episode> GetByIdAsync(int id)
        {
            return await _context.Episodes.FindAsync(id);
        }

        public async Task<Episode> AddAsync(Episode title)
        {
            await _context.Episodes.AddAsync(title);
            await _context.SaveChangesAsync();
            return title;
        }

        public async Task<Episode> UpdateAsync(Episode title)
        {
            _context.Episodes.Update(title);
            await _context.SaveChangesAsync();
            return title;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var title = await _context.Episodes.FindAsync(id);

            if (title == null)
            {
                return false;
            }

            _context.Episodes.Remove(title);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Episode?> GetLastEpisodeByTitleIdAsync(int titleId)
        {
            if (_context.Episodes != null)
            {
                return await _context.Episodes.Where(e => e.TitleId == titleId).OrderByDescending(e => e.Number).FirstOrDefaultAsync();
            }

            return null;
        }
    }
}
