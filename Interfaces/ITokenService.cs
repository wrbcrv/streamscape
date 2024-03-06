using api.Models;

namespace api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Usuario usuario, IList<string> roles);

        DateTime GetTokenExpiration(string token);
    }
}