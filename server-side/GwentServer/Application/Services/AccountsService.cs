using Core.Models.Account;
using Core.Models.Game;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System.Text.RegularExpressions;

namespace Application.Services;

public class AccountsService
{
    private readonly AccountsRepository _accountRepository;
    private readonly DecksConvertorService _decksConvertorService;
    private readonly EncryptService _encryptService;

    public AccountsService(IRepository<AccountEntity> accountRepository, DecksConvertorService decksConvertorService, EncryptService encryptService)
    {
        _accountRepository = (AccountsRepository)accountRepository;
        _decksConvertorService = decksConvertorService;
        _encryptService = encryptService;
    }

    public async Task<(Account? Value, string Error)> Create(string login, string name, string email, string password)
    {
        string error = await _validateAccount(login, email, password);

        if (!string.IsNullOrEmpty(error))
            return (null, error);

        AccountEntity? accountEntity = new AccountEntity()
        {
            Login = login,
            Name = name,
            Email = email,
            HashedPassword = _encryptService.GetHashedPassword(password),
            Decks = _decksConvertorService.ConvertDeckToIds([..Deck.Default]),
        };

        accountEntity = await _accountRepository.AddAsync(accountEntity);

        if (accountEntity == null)
        {
            error = "ACCOUNT_ALREADY_EXIST";
            return (null, error);
        }

        Account account = Account.Create(accountEntity.Id, accountEntity.Login, accountEntity.Name, accountEntity.Email, accountEntity.HashedPassword, [..Deck.Default]);

        return (account, error);
    }

    public async Task Update(int id, string login, string name, string email, string password, List<Deck> decks)
    {
        AccountEntity accountEntity = new AccountEntity()
        {
            Id = id,
            Login = login,
            Name = name,
            Email = email,
            HashedPassword = password,
            Decks = _decksConvertorService.ConvertDeckToIds(decks)
        };

        await _accountRepository.UpdateAsync(accountEntity);
    }
    
    public async Task<Account?> GetAccountByLogin(string login)
    {
        var entity = await _accountRepository.GetByLoginAsync(login);

        return _castToAccount(entity);
    }

    public async Task<Account?> GetAccountByEmail(string email)
    {
        var entity = await _accountRepository.GetByEmailAsync(email);

        return _castToAccount(entity);
    }

    public async Task<Account?> GetAccountById(int id)
    {
        var entity = await _accountRepository.GetByIdAsync(id);

        return _castToAccount(entity);
    }

    private Account? _castToAccount(AccountEntity? accountEntity)
    {
        if (accountEntity == null)
            return null;

        return Account.Create(accountEntity.Id, accountEntity.Login, accountEntity.Name, accountEntity.Email, accountEntity.HashedPassword, _decksConvertorService.ConvertIdsToDecks(accountEntity.Decks));
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
