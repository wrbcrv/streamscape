using api.Data;
using api.DTOs.Titulo;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TituloRepository : ITituloRepository
    {
        private readonly ApplicationDbContext _context;

        public TituloRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Titulo>> GetAllAsync()
        {
            return await _context.Titulos.ToListAsync();
        }

        public async Task<Titulo> CreateAsync(TituloReqDTO request)
        {
            var titulo = new Titulo
            {
                TituloStr = request.Titulo,
                Sinopse = request.Sinopse,
                Lancamento = request.Lancamento
            };

            await _context.Titulos.AddAsync(titulo);
            await _context.SaveChangesAsync();

            return titulo;
        }
    }
}