namespace Application.Services;

public class EncryptService
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