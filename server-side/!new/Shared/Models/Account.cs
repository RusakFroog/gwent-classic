using System.Text.Json.Serialization;

namespace Shared.Models;

public class Account
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Login { get; private set; }
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    public List<Deck> Decks { get; private set; }

    [JsonConstructor]
    public Account(Guid id, string login, string name, string email, string password, List<Deck> decks)
    {
        Id = id;
        Login = login;
        Name = name;
        Email = email;
        HashedPassword = password;
        Decks = decks;
    }

    public static Account Create(Guid id, string login, string name, string email, string hashedPassword, List<Deck> decks)
    {
        return new Account(id, login, name, email, hashedPassword, decks);
    }
}
