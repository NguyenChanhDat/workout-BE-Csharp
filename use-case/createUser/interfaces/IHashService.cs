public interface IHashService
{
    Task<string> HashPassword(string password);
    Task<bool> VerifyPassword(string password, string hashedPassword);
}