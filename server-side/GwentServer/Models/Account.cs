namespace GwentServer.Models;

public class Account
{
    public Guid Id { get; set; }
    public readonly string Login;
    public readonly string Email;
    public readonly string Password;
    
    /// <summary>
    /// Constructor for EFC
    /// </summary>
    private Account() { }
    
    private Account(string login, string email, string password)
    {
        Login = login;
        Email = email;
        Password = _getHashedPassword(password);
    }

    public static Account Create(string login, string email, string password)
    {
        return new Account(login, email, password);
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, Password);
    }
    
    private string _getHashedPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
