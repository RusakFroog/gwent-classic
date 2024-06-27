using Core.Enums.Game;
using Core.Models.Game.Cards;
using Core.Models.Game.Cards.Special;
using Core.Models.Game.Cards.Weather;
using DataAccess.DbContexts;
using DataAccess.Entities;

namespace DataAccess.Repositories;

public class CardsRepository
{
    private readonly CardDbContext _context;
    private readonly Dictionary<int, Card> _cards;
    
    public CardsRepository(CardDbContext cardDbContext)
    {
        _context = cardDbContext;
        _cards = _getCards();

        _add(new HornCard());
        _add(new DecoyCard());
        _add(new FogCard());
        _add(new FrostCard());
        _add(new RainCard());
        _add(new SkelligeStormCard());
    }

    public Card GetById(int id)
    {
        _cards.TryGetValue(id, out Card? value);

        ArgumentNullException.ThrowIfNull(value, $"Card with id {id} was not found");

        return value!;
    }

    public int GetIdByCard<T>() where T : Card
    {
        return _cards.FirstOrDefault(x => x.Value is T).Key;
    }

    private void _add(Card card)
    {
        _cards.Add(_cards.Count, card);
    }

    private Dictionary<int, Card> _getCards()
    {
        Dictionary<int, Card> result = new();

        Fraction[] fractions = [Fraction.None, Fraction.Nilfgaardian, Fraction.Skellige, Fraction.Monsters, Fraction.Scoiatael, Fraction.NorthenRealms];
        
        Console.WriteLine("Start loading decks");

        foreach (var fraction in fractions)
        {
            Console.WriteLine("Loading deck for " + fraction + "...");

            foreach (var card in _getDeckByFraction(fraction))
                result.Add(card.Id, Card.CreateCard(card.Id, card.Name, (sbyte)card.Strength, card.Fraction, card.FieldLines, card.CanBeTaken, card.HornBoost, card.WeatherImmunity));
        }

        Console.WriteLine("Finish loading decks");

        return result;
    }

    private IEnumerable<CardEntity> _getDeckByFraction(Fraction fraction)
    {
        return _context.Cards.Where(x => x.Fraction == fraction);
    }
}
