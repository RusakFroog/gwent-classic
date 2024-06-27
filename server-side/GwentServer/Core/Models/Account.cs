using Core.Models.Game.Decks;

namespace Core.Models;

public class Account
{
    public const int MAX_LENGTH_LOGIN = 40;
    public const int MAX_LENGTH_NAME = 20;

    public readonly Guid Id;
    public readonly string Name;
    public readonly string Login;
    public readonly string Email;
    public readonly string Password;
    public readonly List<Deck> Decks;

    private Account(Guid? id, string login, string name, string email, string password, List<Deck>? decks = null)
    {
        Id = id ?? Guid.NewGuid();
        Login = login;
        Name = name;
        Email = email;
        Password = password;
        Decks = decks ?? [..Deck.Default];
    }

    public static Account Create(string login, string name, string email, string password, List<Deck>? decks = null, bool hashPassword = false, Guid? id = null)
    {
        string hashedPassword = hashPassword ? _getHashedPassword(password) : password;
        
        return new Account(id, login, name, email, hashedPassword, decks);
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
