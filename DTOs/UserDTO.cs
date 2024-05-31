using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Username é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome de usuário deve ter entre 3 e 50 caracteres.")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Senha é obrigatória.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres.")]
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}