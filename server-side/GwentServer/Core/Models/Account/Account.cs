using Core.Models.Game.Decks;

namespace Core.Models.Account;

public class Account
{
    public const int MAX_LENGTH_LOGIN = 40;
    public const int MAX_LENGTH_NAME = 20;

    public readonly int Id;
    public readonly string Name;
    public readonly string Login;
    public readonly string Email;
    public readonly string HashedPassword;
    public readonly List<Deck> Decks;

    private Account(int id, string login, string name, string email, string password, List<Deck> decks)
    {
        Id = id;
        Login = login;
        Name = name;
        Email = email;
        HashedPassword = password;
        Decks = decks;
    }

    public static Account Create(int id, string login, string name, string email, string hashedPassword, List<Deck> decks)
    {
        return new Account(id, login, name, email, hashedPassword, decks);
    }
}
