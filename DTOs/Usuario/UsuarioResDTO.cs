namespace api.DTOs.Usuario
{
    public class UsuarioResDTO
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        public static UsuarioResDTO ValueOf(Models.Usuario usuario)
        {
            if (usuario == null)
                return null;

            return new UsuarioResDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Username = usuario.UserName,
                Email = usuario.Email
            };
        }
    }
}