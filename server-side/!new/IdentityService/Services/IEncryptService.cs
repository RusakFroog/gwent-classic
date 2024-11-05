namespace IdentityService.Services;

public interface IEncryptService
{
    bool VerifyPassword(string password, string hashedPassword);
    string GetHashedPassword(string value);
}
