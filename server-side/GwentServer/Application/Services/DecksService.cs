using Core.Enums.Game;
using Core.Models.Game.Cards;
using Core.Models.Game.Decks;
using DataAccess.Repositories;

namespace Application.Services;

public class DecksService
{
    private readonly AccountsService _accountsService;
    private readonly CardsRepository _cardsRepository;

    public DecksService(CardsRepository cardsRepository, AccountsService accountsService)
    {
        _cardsRepository = cardsRepository;
        _accountsService = accountsService;
    }

    public async Task<DeckDTO> GetDeckByFraction(string userId, Fraction fraction)
    {
        var account = await _accountsService.GetAccountById(Guid.Parse(userId));

        ArgumentNullException.ThrowIfNull(account, "Account not found");

        return new DeckDTO(fraction, account.Decks
            .FirstOrDefault(d => d.Fraction == fraction)!.Cards
            .Select(c => c.Id));
    }

    public async Task<string> UpdateDeck(string userId, DeckDTO deck)
    {
        string error = string.Empty;

        var account = await _accountsService.GetAccountById(Guid.Parse(userId));

        if (account == null)
        {
            error = "Account was not found";
            return error;
        }

        Deck foundDeck = account.Decks.FirstOrDefault(d => d.Fraction == deck.Fraction)!;
        foundDeck.Cards.Clear();
        foundDeck.Cards.AddRange(deck.CardIds.Select(_cardsRepository.GetById));

        await _accountsService.Update(account.Id, null, null, null, account.Decks);

        return error;
    }
}
