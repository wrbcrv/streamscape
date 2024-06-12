using Api.Models;
using System.Collections.Generic;

namespace Api.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<MyListResponseDTO> MyList { get; set; } = new List<MyListResponseDTO>();

        public static UserResponseDTO ValueOf(User user)
        {
            return new UserResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                MyList = user.MyList.Select(MyListResponseDTO.ValueOf).ToList()
            };
        }
    }
}
