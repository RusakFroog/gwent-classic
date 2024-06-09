using Core.Models;
using DataAccess.DbContexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AccountsRepository
{
    public readonly AccountDbContext _context;
    
    public AccountsRepository(AccountDbContext accountDbContext)
    {
        _context = accountDbContext;
    }

    public async Task<List<Account>> GetAll()
    {
        var accountEntities = await _context.Accounts
            .AsNoTracking()
            .ToListAsync();

        var accounts = accountEntities
            .Select(a => Account.Create(a.Login, a.Name, a.Email, a.Password, false, a.Id))
            .ToList();

        return accounts;
    }

    public async Task<Guid> Create(Account account)
    {
        var accountEntity = new AccountEntity
        {
            Id = account.Id,
            Login = account.Login,
            Name = account.Name,
            Email = account.Email,
            Password = account.Password
        };

        await _context.Accounts.AddAsync(accountEntity);
        await _context.SaveChangesAsync();

        return accountEntity.Id;
    }

    public async Task<Guid> Update(Guid id, string? login, string? name, string? email)
    {
        await _context.Accounts
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Email, a => string.IsNullOrEmpty(email) ? a.Email : email)
                .SetProperty(a => a.Name, a => string.IsNullOrEmpty(name) ? a.Email : name)
                .SetProperty(a => a.Login, a => string.IsNullOrEmpty(login) ? a.Login : login)
            );

        return id;
    }

    public async Task<Guid> UpdatePassword(Guid id, string password)
    {
        await _context.Accounts
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Password, a => password)
            );

        return id;
    }
    
    public async Task<Guid> Delete(Guid id)
    {
        await _context.Accounts
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
