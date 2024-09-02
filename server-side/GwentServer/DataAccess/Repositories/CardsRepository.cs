using Core.Enums.Game;
using Core.Entities.Game.Cards;
using Core.Entities.Game.Cards.Special;
using Core.Entities.Game.Cards.Weather;
using DataAccess.Databases;
using DataAccess.Entities;

namespace DataAccess.Repositories;

public class CardsRepository : Repository<CardEntity>
{
    protected override string _table => "cards";

    private readonly Dictionary<int, Card> _loadedCards;
    
    public CardsRepository(MySqlDbContext database) : base(database)
    {
        _loadedCards = _getCards().Result;

        _addLoaded(Card.CreateCard<HornCard>(0));
        _addLoaded(Card.CreateCard<DecoyCard>(1));
        _addLoaded(Card.CreateCard<FogCard>(2));
        _addLoaded(Card.CreateCard<FrostCard>(3));
        _addLoaded(Card.CreateCard<RainCard>(4));
        _addLoaded(Card.CreateCard<SkelligeStormCard>(5));
    }

    public Card GetCardById(int id)
    {
        _loadedCards.TryGetValue(id, out Card? value);

        ArgumentNullException.ThrowIfNull(value, $"Card with id {id} was not found");

        return value!;
    }

    public int GetIdByCard<T>() where T : Card
    {
        return _loadedCards.FirstOrDefault(x => x.Value is T).Key;
    }

    private void _addLoaded(Card card)
    {
        _loadedCards.Add(_loadedCards.Count, card);
    }

    private async Task<Dictionary<int, Card>> _getCards()
    {
        Dictionary<int, Card> result = [];

        Fraction[] fractions = [Fraction.None, Fraction.Nilfgaardian, Fraction.Skellige, Fraction.Monsters, Fraction.Scoiatael, Fraction.NorthenRealms];
        
        Console.WriteLine("Start loading decks");

        foreach (var fraction in fractions)
        {
            Console.WriteLine("Loading deck for " + fraction + "...");

            var deck = await _getDeckByFraction(fraction);

            foreach (var card in deck)
                result.Add(card.Id, Card.CreateCard(card.Id, card.Name, (sbyte)card.Strength, card.Fraction, card.FieldLines, card.CanBeTaken, card.HornBoost, card.WeatherImmunity));
        }

        Console.WriteLine("Finish loading decks");

        return result;
    }

    private async Task<IEnumerable<CardEntity>> _getDeckByFraction(Fraction fraction)
    {
        var dataTable = await _database.QueryAsync($"SELECT * FROM `{_table}` WHERE `fraction` = '{fraction}'");

        if (dataTable == null || dataTable.Rows.Count == 0)
            return [];

        List<CardEntity> result = [];

        foreach (System.Data.DataRow row in dataTable.Rows)
        {
            var item = _getParsedItem(dataTable.Columns, row);

            if (item != null)
                result.Add(item);
        }

        return result;
    }
}
