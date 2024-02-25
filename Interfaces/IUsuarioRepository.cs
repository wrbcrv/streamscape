using api.DTOs.Usuario;
using api.Models;

namespace api.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync();

        Task<Usuario?> GetByIdAsync(string id);

        Task<Usuario> CreateAsync(UsuarioReqDTO request);

        Task<Usuario> UpdateAsync(string id, UsuarioReqDTO request);

        Task<Usuario> DeleteAsync(string id);

        Task<Usuario?> FindByEmailAndSenhaAsync(string email, string senha);

        Task<Usuario?> FindByEmail(string email);
    }
}