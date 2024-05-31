namespace Api.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string hashed, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashed);
        }
    }
}