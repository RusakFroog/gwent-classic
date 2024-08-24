using Core.Enums.Game;
using Core.Models.Game;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace Application.Services;

public class DecksService
{
    private readonly CardsRepository _cardsRepository;
    private readonly AccountsService _accountsService;

    public DecksService(IRepository<CardEntity> cardsRepository, AccountsService accountsService)
    {
        _cardsRepository = (CardsRepository)cardsRepository;
        _accountsService = accountsService;
    }

    public async Task<(DeckDTO? Value, string Error)> GetDeckByFraction(int userId, Fraction fraction)
    {
        var account = await _accountsService.GetAccountById(userId);

        if (account == null)
            return (null, "ACCOUNT_DOES_NOT_EXIST");

        return (new DeckDTO(fraction, account.Decks.FirstOrDefault(d => d.Fraction == fraction)!.Cards.Select(c => c.Id)), string.Empty);
    }

    public async Task<string> UpdateDeck(int userId, DeckDTO deck)
    {
        var account = await _accountsService.GetAccountById(userId);

        if (account == null)
            return "ACCOUNT_DOES_NOT_EXIST";

        Deck foundDeck = account.Decks.FirstOrDefault(d => d.Fraction == deck.Fraction)!;
        foundDeck.Cards.Clear();
        foundDeck.Cards.AddRange(deck.CardIds.Select(_cardsRepository.GetCardById));

        await _accountsService.Update(account.Id, account.Login, account.Name, account.Email, account.HashedPassword, account.Decks);

        return string.Empty;
    }
}
