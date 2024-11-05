using Core.Enums.Game;
using Core.Entities.Game;
using Core.Entities.Database;
using DataAccess.Repositories;
using Core.Interfaces;
using Core.Entities.Game.Cards;

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

    public async Task<(DeckDTO Value, string Error)> GetDeckByFraction(int userId, Fraction fraction)
    {
        var account = await _accountsService.GetAccountById(userId);

        if (account == null)
            return (null, "ACCOUNT_DOES_NOT_EXIST");

        IEnumerable<CardDTO> requestedCards = account.Decks
            .FirstOrDefault(d => d.Fraction == fraction)
            .Cards.Select(_createCardDTO);

        return (new DeckDTO(fraction, requestedCards), string.Empty);
    }

    public async Task<(DeckDTO Value, string Error)> GetCardsPoolByFraction(int userId, Fraction fraction)
    {
        var account = await _accountsService.GetAccountById(userId);

        if (account == null)
            return (null, "ACCOUNT_DOES_NOT_EXIST");

        var accountDeck = account.Decks.FirstOrDefault(d => d.Fraction == fraction);

        var allCards = _cardsRepository.GetCardsByFraction(fraction)
            .Concat(_cardsRepository.GetCardsByFraction(Fraction.None));

        var pool = allCards.Where(c => !accountDeck.Cards.Contains(c))
                           .Select(_createCardDTO);

        return (new DeckDTO(fraction, pool), string.Empty);
    }

    public async Task<string> UpdateDeck(int userId, Fraction fraction, IEnumerable<int> deckWithIds)
    {
        var account = await _accountsService.GetAccountById(userId);

        if (account == null)
            return "ACCOUNT_DOES_NOT_EXIST";

        Deck newDeck = new Deck(fraction, deckWithIds.Select(_cardsRepository.GetCardById).ToList());
        int indexOfDeck = account.Decks.FindIndex(d => d.Fraction == fraction);

        account.Decks[indexOfDeck] = newDeck;

        await _accountsService.Update(account.Id, account.Login, account.Name, account.Email, account.HashedPassword, account.Decks);

        return string.Empty;
    }

    private CardDTO _createCardDTO(Card card) =>
        new CardDTO(card.Id, card.Fields.Select(f => f.ToString()), card.CardCategory.ToString());
}
