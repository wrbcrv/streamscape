using Api.DTOs;

namespace Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDTO>> GetAllAsync();
        Task<UserResponseDTO> GetByIdAsync(int id);
        Task<UserResponseDTO> AddAsync(UserDTO userDto);
        Task<UserResponseDTO> UpdateAsync(int id, UserDTO userDto);
        Task<bool> DeleteAsync(int id);
        Task<UserResponseDTO> GetByUsernameAndPassword(string username, string password);
    }
}