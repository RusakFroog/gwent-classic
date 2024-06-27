using Core.Enums.Game;
using Core.Models;
using Core.Models.Game.Cards;
using Core.Models.Game.Decks;
using DataAccess.DbContexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AccountsRepository
{
    private readonly AccountDbContext _context;
    private readonly CardsRepository _cardsRepository;

    public AccountsRepository(AccountDbContext accountDbContext, CardsRepository cardsRepository)
    {
        _context = accountDbContext;
        _cardsRepository = cardsRepository;
    }

    public async Task<List<Account>> GetAll()
    {
        var accountEntities = await _context.Accounts
            .AsNoTracking()
            .ToListAsync();

        var accounts = accountEntities
            .Select(_castToAccount)
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
            Password = account.Password,
            Decks = Deck.ConvertToIds(Deck.Default)
        };

        await _context.Accounts.AddAsync(accountEntity);
        await _context.SaveChangesAsync();

        return accountEntity.Id;
    }

    public async Task<Guid> Update(Guid id, string? login, string? name, string? email, IEnumerable<Deck>? decks)
    {
        await _context.Accounts
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Email, a => string.IsNullOrEmpty(email) ? a.Email : email)
                .SetProperty(a => a.Name, a => string.IsNullOrEmpty(name) ? a.Email : name)
                .SetProperty(a => a.Login, a => string.IsNullOrEmpty(login) ? a.Login : login)
                .SetProperty(a => a.Decks, a => decks == null ? a.Decks : Deck.ConvertToIds(decks))
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

    private Account _castToAccount(AccountEntity accountEntity)
    {
        List<Deck> decks = [];

        foreach (var deck in accountEntity.Decks)
        {
            List<Card> cards = [];

            Fraction fraction = deck.Key;
            var cardsId = deck.Value;

            foreach (int cardId in cardsId)
            {
                Card? card = _cardsRepository.GetById(cardId);

                ArgumentNullException.ThrowIfNull(card);

                cards.Add(card);
            }

            decks.Add(new Deck(fraction, cards));
        }

        return Account.Create(accountEntity.Login, accountEntity.Name, accountEntity.Email, accountEntity.Password, decks, false, accountEntity.Id);
    }
}
