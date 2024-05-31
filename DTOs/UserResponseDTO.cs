using Api.Models;

namespace Api.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public Role Role { get; set; }

        public static UserResponseDTO ValueOf(User user)
        {
            return new UserResponseDTO
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}