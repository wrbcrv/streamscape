using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}