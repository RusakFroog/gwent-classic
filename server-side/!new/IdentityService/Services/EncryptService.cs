namespace IdentityService.Services;

public class EncryptService : IEncryptService
{
    public EncryptService()
    {

    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }

    public string GetHashedPassword(string value)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(value);
    }
}