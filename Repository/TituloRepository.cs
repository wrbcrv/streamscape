using api.Data;
using api.DTOs.Titulo;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TituloRepository : ITituloRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileRepository _fileRepository;

        public TituloRepository(ApplicationDbContext context, IFileRepository fileRepository)
        {
            _context = context;
            _fileRepository = fileRepository;
        }

        public async Task<Titulo?> GetByIdAsync(int id)
        {
            return await _context.Titulos.FindAsync(id);
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
                Lancamento = request.Lancamento,
                Generos = request.Generos,
                Classificacao = request.Classificacao
            };

            await _context.Titulos.AddAsync(titulo);
            await _context.SaveChangesAsync();

            return titulo;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var titulo = await _context.Titulos.FindAsync(id);

            if (titulo == null)
            {
                return false;
            }

            _context.Titulos.Remove(titulo);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Titulo> AddImageAsync(int tituloId, IFormFile image)
        {
            var titulo = await _context.Titulos.FindAsync(tituloId) ?? throw new ArgumentException("Título não encontrado.");

            if (!_fileRepository.IsImage(image.FileName))
            {
                throw new ArgumentException("Somente arquivos de imagem são permitidos.");
            }

            string folder = Path.Combine("C:\\", "Uploads");
            string thumbPath = await _fileRepository.SaveFileAsync(image, folder);
            titulo.Thumb = thumbPath;

            _context.Titulos.Update(titulo);
            await _context.SaveChangesAsync();

            return titulo;
        }

        public async Task<Titulo> AddBannerAsync(int tituloId, IFormFile banner)
        {
            var titulo = await _context.Titulos.FindAsync(tituloId) ?? throw new ArgumentException("Título não encontrado.");

            if (!_fileRepository.IsImage(banner.FileName))
            {
                throw new ArgumentException("Somente arquivos de imagem são permitidos.");
            }

            string folder = Path.Combine("C:\\", "Banners");
            string bannerPath = await _fileRepository.SaveFileAsync(banner, folder);
            titulo.Banner = bannerPath;

            _context.Titulos.Update(titulo);
            await _context.SaveChangesAsync();

            return titulo;
        }

        public async Task<byte[]> DownloadBannerAsync(int tituloId)
        {
            var titulo = await _context.Titulos.FindAsync(tituloId) ?? throw new ArgumentException("Título não encontrado.");

            if (string.IsNullOrEmpty(titulo.Banner))
            {
                throw new ArgumentException("O banner do título não está disponível.");
            }

            return _fileRepository.DownloadFile(titulo.Banner);
        }


        public async Task<byte[]> DownloadImageAsync(int tituloId)
        {
            var titulo = await _context.Titulos.FindAsync(tituloId) ?? throw new ArgumentException("Título não encontrado.");

            if (string.IsNullOrEmpty(titulo.Thumb))
            {
                throw new ArgumentException("A imagem do título não está disponível.");
            }

            return _fileRepository.DownloadFile(titulo.Thumb);
        }
    }
}