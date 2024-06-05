using BCrypt.Net;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

[Table("accounts")]
public class Account
{
    public const int MAX_LENGTH_LOGIN = 40;
    
    public Guid Id { get; set; }
    public readonly string Login;
    public readonly string Email;
    public readonly string Password;
    
    private Account(string login, string email, string password)
    {
        Login = login;
        Email = email;
        Password = password;
    }

    public static Account Create(string login, string email, string password, bool hashPassword = false)
    {
        return new Account(login, email, hashPassword ? _getHashedPassword(password) : password);
    }

    public bool VerifyPassword(string password)
    {
        var result = BCrypt.Net.BCrypt.EnhancedVerify(password, Password);

        return result;
    }
    
    private static string _getHashedPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }
}
