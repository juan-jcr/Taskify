namespace Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        bool Verify(string password, string hash);
        string HashPassword(string password);
    }
}

