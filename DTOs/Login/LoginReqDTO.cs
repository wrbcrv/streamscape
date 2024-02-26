using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Login
{
    public class LoginReqDTO
    {
        [EmailAddress(ErrorMessage = "Seu e-mail deve ser neste formato: nome@example.com.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite sua senha.")]
        public string Senha { get; set; } = string.Empty;
    }
}