using Core.Models;
using DataAccess.Repositories;
using System.Text.RegularExpressions;

namespace Application.Services;

public class AccountsService
{
    private readonly AccountsRepository _repository;
    
    public AccountsService(AccountsRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Create new account with validation
    /// </summary>
    /// <param name="login">string</param>
    /// <param name="name">string</param>
    /// <param name="email">string</param>
    /// <param name="password">string</param>
    /// <returns>
    /// <para>An error message</para>
    /// - string.Empty mean not errors
    /// </returns>
    public async Task<(Account?, string)> Create(string login, string name, string email, string password)
    {
        string error = await _validateAccount(login, email, password);

        if (!string.IsNullOrEmpty(error))
            return (null, error);

        Account account = Account.Create(login, name, email, password, true);
        
        await _repository.Create(account);

        return (account, error);
    }

    public async Task Update(Guid id, string login, string name, string email)
    {
        await _repository.Update(id, login, name, email);
    }
    
    public async Task<Account?> GetAccountByLogin(string login)
    {
        var accounts = await _repository.GetAll();
        
        return accounts.FirstOrDefault(a => a.Login.ToLower() == login.ToLower());
    } 

    public async Task<Account?> GetAccountByEmail(string email)
    {
        var accounts = await _repository.GetAll();
        
        return accounts.FirstOrDefault(a => a.Email.ToLower() == email.ToLower());
    }

    public async Task<Account?> GetAccountById(Guid id)
    {
        var accounts = await _repository.GetAll();
        
        return accounts.FirstOrDefault(a => a.Id == id);
    }

    private async Task<string> _validateAccount(string login, string email, string password)
    {
        if (login.Length < 4 || login.Length > 40)
            return "LENGTH_LOGIN";
        
        if (password.Length < 6)
            return "LENGTH_PASSWORD";
        
        if (!Regex.IsMatch(login, @"^[a-z0-9]+$"))
            return "ONLY_ALPHANUMERIC_LOGIN";
        
        if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
            return "ONLY_ALPHANUMERIC_PASSWORD";

        if (!email.Contains("@") || !Regex.IsMatch(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            return "INVALID_EMAIL";

        if (await GetAccountByEmail(email) != null)
            return "EXIST_EMAIL";
        
        if (await GetAccountByLogin(login) != null)
            return "EXIST_LOGIN";

        return string.Empty;
    }
}
