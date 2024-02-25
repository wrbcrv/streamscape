using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Login
{
    public class LoginReqDTO
    {
        [Required(ErrorMessage = "E-mail é obrigatório.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatório.")]
        public string Senha { get; set; } = string.Empty;
    }
}