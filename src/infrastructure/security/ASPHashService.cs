using Microsoft.AspNetCore.Identity;

public class ASPHashService : IHashService
{
    private readonly PasswordHasher<object> _hasher = new();

    public Task<string> HashPassword(string password)
    {
        return Task.FromResult(_hasher.HashPassword(null!, password));
    }

    public Task<bool> VerifyPassword(string hashed, string provided)
    {
        var result = _hasher.VerifyHashedPassword(null!, hashed, provided);
        return Task.FromResult(result == PasswordVerificationResult.Success);
    }

}