using Application.Common.Interfaces;


namespace Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher

    {
        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
        
    }
}

