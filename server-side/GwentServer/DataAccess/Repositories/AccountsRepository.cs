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
            .Select(a => Account.Create(a.Login, a.Email, a.Password))
            .ToList();

        return accounts;
    }

    public async Task<Guid> Create(Account account)
    {
        var accountEntity = new AccountEntity
        {
            Id = account.Id,
            Email = account.Email,
            Login = account.Login,
            Password = account.Password
        };

        await _context.Accounts.AddAsync(accountEntity);
        await _context.SaveChangesAsync();

        return accountEntity.Id;
    }

    public async Task<Guid> Update(Guid id, string login, string email, string password)
    {
        await _context.Accounts
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Password, a => password)
                .SetProperty(a => a.Email, a => email)
                .SetProperty(a => a.Login, a => login)
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
