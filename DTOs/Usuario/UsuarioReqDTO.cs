using System.ComponentModel.DataAnnotations;
using api.Models;

namespace api.DTOs.Usuario
{
    public class UsuarioReqDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sobrenome é obrigatório.")]
        public string Sobrenome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nome de usuário é obrigatório.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Seu e-mail deve ser neste formato: nome@example.com.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatório.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Senha { get; set; } = string.Empty;
    }
}