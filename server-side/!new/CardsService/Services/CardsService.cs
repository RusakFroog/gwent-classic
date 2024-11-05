using CardsService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Cards;
using Shared.Models;

namespace CardsService.Services;

public class CardsService : ICardsService
{
    private readonly CardDbContext _context;

    public CardsService(CardDbContext context)
    {
        _context = context;
    }

    public async Task<GetCardsFromIdResponse> GetCardsFromIds(List<int> cardsId)
    {
        var cardsEntity = await _context.Cards
            .AsNoTracking()
            .Where(c => cardsId.Contains(c.Id))
            .ToListAsync();

        var cards = new List<Card>();

        foreach (var card in cardsEntity)
            cards.Add(new Card(card.Id, card.Strength, card.Fraction, card.CardCategory, card.FieldLines, card.IsHero));

        return new GetCardsFromIdResponse(cards);
    }

    public GetIdsFromCardResponse GetIdFromCards(List<Card> cards)
    {
        var cardsId = cards.Select(c => c.Id).ToList();

        return new GetIdsFromCardResponse(cardsId);
    }
}
