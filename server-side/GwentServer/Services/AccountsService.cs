using GwentServer.DataAccess;
using GwentServer.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GwentServer.Services;

public class AccountsService
{
    private readonly ApplicationDbContext _dbContext;
    
    public AccountsService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Create new account with validation
    /// </summary>
    /// <param name="login">string</param>
    /// <param name="email">string</param>
    /// <param name="password">string</param>
    /// <returns>
    /// <para>An error message</para>
    /// - string.Empty mean not errors
    /// </returns>
    public async Task<string> Create(string login, string email, string password)
    {
        string error = await _validateAccount(login, email, password);

        if (!string.IsNullOrEmpty(error))
            return error;
        
        Account createdAccount = Account.Create(login, email, password);
        
        await _dbContext.Accounts.AddAsync(createdAccount);
        await _dbContext.SaveChangesAsync();

        return String.Empty;
    }

    public async Task<Account?> GetAccountByLogin(string login) => await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Login.ToLower() == login.ToLower());
    public async Task<Account?> GetAccountByEmail(string email) => await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());

    private async Task<string> _validateAccount(string login, string email, string password)
    {
        if (login.Length < 4)
            return "Login must contains at least 4 characters";
        
        if (password.Length < 6)
            return "Password must contains at least 6 characters";
        
        if (!Regex.IsMatch(login, @"^[a-zA-Z0-9]+$"))
            return "Login must contains only a-Z, 0-9";
        
        if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
            return "Password must contains only a-Z, 0-9";

        if (!email.Contains("@") || !Regex.IsMatch(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            return "Email is invalid";

        if (await GetAccountByEmail(email) != null)
            return "Account with current Email already registered";
        
        if (await GetAccountByLogin(login) != null)
            return "Account with current Login already registered";

        return string.Empty;
    }
}
