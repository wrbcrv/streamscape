using api.Data;
using api.DTOs.Usuario;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHashService _hashService;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioRepository(ApplicationDbContext context, IHashService hashService, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _hashService = hashService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(string id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> CreateAsync(UsuarioReqDTO request)
        {
            string hashed = _hashService.HashPassword(request.Senha);

            var usuario = new Usuario
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                UserName = request.Username,
                Email = request.Email,
                Senha = hashed
            };

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            await _userManager.AddToRoleAsync(usuario, "User");

            return usuario;
        }

        public async Task<Usuario> UpdateAsync(string id, UsuarioReqDTO request)
        {
            var existing = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (existing == null)
            {
                return null;
            }

            existing.Nome = request.Nome;
            existing.Sobrenome = request.Sobrenome;
            existing.UserName = request.Username;
            existing.Email = request.Email;
            existing.Senha = request.Senha;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<Usuario> DeleteAsync(string id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return null;
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario?> FindByEmailAndSenhaAsync(string email, string senha)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return null;
            }

            if (!_hashService.VerifyPassword(senha, usuario.Senha))
            {
                return null;
            }

            return usuario;
        }

        public async Task<Usuario?> FindByEmail(string email)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }
    }
}