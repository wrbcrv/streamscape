using Api.DTOs;

namespace Api.Services
{
    public interface IAuthService
    {
        string Authenticate(UserResponseDTO userDTO);
    }
}