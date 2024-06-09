using BCrypt.Net;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

[Table("accounts")]
public class Account
{
    public const int MAX_LENGTH_LOGIN = 40;

    public Guid Id { get; set; }
    public readonly string Name;
    public readonly string Login;
    public readonly string Email;
    public readonly string Password;
    
    private Account(Guid? id, string login, string name, string email, string password)
    {
        Id = id ?? Guid.NewGuid();
        Login = login;
        Name = name;
        Email = email;
        Password = password;
    }

    public static Account Create(string login, string name, string email, string password, bool hashPassword = false, Guid? id = null)
    {
        string hashedPassword = hashPassword ? _getHashedPassword(password) : password;
        
        return new Account(id, login, name, email, hashedPassword);
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
