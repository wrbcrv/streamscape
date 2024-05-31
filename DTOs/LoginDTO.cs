using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; } = string.Empty;
    }
}