using IdentityService.DataAccess;
using IdentityService.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.DTOs.Cards;
using Shared.DTOs.Identity;
using Shared.Exceptions;
using Shared.Models;
using Shared.Models.DataContext;

namespace IdentityService.Services;

public class AccountService : IAccountService
{
    private readonly AccountDbContext _context;
    private readonly IBusControl _busControl;
    private readonly IEncryptService _encryptService;

    private readonly Uri _cardsQueue = new Uri("rabbitmq://localhost/CardsQueue");

    public AccountService(AccountDbContext context, IBusControl busControl, IEncryptService encryptService)
    {
        _context = context;
        _busControl = busControl;
        _encryptService = encryptService;
    }

    public async Task<CreateAccountResponse> Create(string login, string name, string email, string password)
    {
        var accountEntityByEmail = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        var accountEntityByLogin = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Login == login);

        if (accountEntityByEmail != null)
            throw new BadRequestException("EXIST_EMAIL");

        if (accountEntityByLogin != null)
            throw new BadRequestException("EXIST_LOGIN");

        string hashedPassword = _encryptService.GetHashedPassword(password);

        Account account = Account.Create(Guid.NewGuid(), login, name, email, hashedPassword, Deck.Default);

        await _context.Accounts.AddAsync(new AccountEntity()
        {
            Id = account.Id,
            Login = account.Login,
            Name = account.Name,
            Email = account.Email,
            HashedPassword = account.HashedPassword,
            Decks = DeckEntity.Default
        });
        await _context.SaveChangesAsync();

        return new CreateAccountResponse(account.Id, account.Login);
    }

    public async Task<LoginAccountResponse> Login(string login, string password)
    {
        var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Login == login);

        if (account == null)
            throw new BadRequestException("LOGIN_DOES_NOT_EXIST");

        bool passwordEquals = _encryptService.VerifyPassword(password, account.HashedPassword);

        if (passwordEquals == false)
            throw new BadRequestException("WRONG_PASSWORD");

        return new LoginAccountResponse(account.Id, account.Name);
    }

    public async Task<Account> GetById(Guid id)
    {
        var accountEntity = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        List<Deck> decks = await _convertDecks(accountEntity.Decks);

        return Account.Create(accountEntity.Id, accountEntity.Login, accountEntity.Name, accountEntity.Email, accountEntity.HashedPassword, decks);
    }

    public async Task<EmptyResponse> Update(Guid id, string login, string name, string email, string password, List<DeckEntity> decks)
    {
        var accountEntity = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id) 
            ?? throw new BadRequestException("ACCOUNT_DOES_NOT_EXIST");

        accountEntity.Login = string.IsNullOrEmpty(login) ? accountEntity.Login : login;
        accountEntity.Name = string.IsNullOrEmpty(name) ? accountEntity.Name : name;
        accountEntity.Email = string.IsNullOrEmpty(email) ? accountEntity.Email : email;
        accountEntity.HashedPassword = string.IsNullOrEmpty(password) ? accountEntity.HashedPassword : _encryptService.GetHashedPassword(password);
        
        if (decks != null)
        {
            foreach (var d in decks)
            {
                var deck = accountEntity.Decks.FirstOrDefault(x => x.Fraction == d.Fraction);
                deck.CardsId = d.CardsId;
            }

            accountEntity.Decks = accountEntity.Decks.ToList(); // For detect that ".Decks" is updated for EFC JsonB
        }

        await _context.SaveChangesAsync();

        return new EmptyResponse();
    }

    private async Task<List<Deck>> _convertDecks(IEnumerable<DeckEntity> decks)
    {
        var client = _busControl.CreateRequestClient<GetCardsFromIdRequest>(_cardsQueue);

        var deckTasks = decks.Select(async (deckEntity) =>
        {
            var cards = (await client.GetResponse<GetCardsFromIdResponse>(new GetCardsFromIdRequest(deckEntity.CardsId))).Message.Cards;

            return new Deck(deckEntity.Fraction, cards);
        });

        return [..(await Task.WhenAll(deckTasks))];
    }
}
