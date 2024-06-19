using Api.DTOs;

namespace Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDTO>> GetAllAsync();
        Task<UserResponseDTO> GetByIdAsync(int id);
        Task<UserResponseDTO> AddAsync(UserDTO userDto);
        Task<UserResponseDTO> UpdateAsync(int id, UserUpdateDTO userDTO);
        Task<bool> DeleteAsync(int id);
        Task<UserResponseDTO> GetByUsernameOrEmailAndPassword(string usernameOrEmail, string password);
        Task<(UserResponseDTO user, string message)> AddToMyList(int uid, int tid);
        Task<(UserResponseDTO user, string message)> RemoveFromMyList(int uid, int tid);
    }
}