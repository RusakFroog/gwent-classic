using Core.Enums.Game;
using Core.Entities.Database;
using Core.Entities.Game.Cards;
using Core.Entities.Game.Cards.Special;
using Core.Entities.Game.Cards.Weather;
using DataAccess.Databases;
using System.Text.Json;

namespace DataAccess.Repositories;

public class CardsRepository : Repository<CardEntity>
{
    protected override string _table => "cards";

    private readonly Dictionary<int, Card> _loadedCards;

    public CardsRepository(MySqlDbContext database) : base(database)
    {
        _loadedCards = _getCards().Result;

        _loadCard(Card.CreateCard<CowCard>(618));
        _loadCard(Card.CreateCard<DandelionCard>(619));

        _loadCard(Card.CreateCard<DecoyCard>(1001));
        _loadCard(Card.CreateCard<HornCard>(1002));
        _loadCard(Card.CreateCard<FogCard>(1003));
        _loadCard(Card.CreateCard<FrostCard>(1004));
        _loadCard(Card.CreateCard<RainCard>(1005));
        _loadCard(Card.CreateCard<SkelligeStormCard>(1006));
        _loadCard(Card.CreateCard<ClearWeatherCard>(1007));
        _loadCard(Card.CreateCard<ScorchCard>(1008));
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

    private void _loadCard(Card card)
    {
        _loadedCards.Add(card.Id, card);
    }

    private async Task<Dictionary<int, Card>> _getCards()
    {
        Dictionary<int, Card> result = [];

        Fraction[] fractions = [Fraction.None, Fraction.Nilfgaardian, Fraction.Skellige, Fraction.Monsters, Fraction.Scoiatael, Fraction.NorthenRealms];
        
        Console.WriteLine("Start loading decks");

        foreach (var fraction in fractions)
        {
            Console.WriteLine($"Loading deck for {fraction}...");

            var deck = await _getCardsByFraction(fraction);

            foreach (var card in deck)
                result.Add(card.Id, card);
        }

        Console.WriteLine("Finish loading decks");

        return result;
    }

    private async Task<IEnumerable<Card>> _getCardsByFraction(Fraction fraction)
    {
        var dataTable = await _database.QueryAsync
        (
            $"SELECT {_table}.id, {_table}.type_id, {_table}.strength, {_table}.field_lines, {_table}.is_hero, {_table}.muster_cards, {_table}.card_category, fractions.name AS fraction " +
            $"FROM {_table} " +
            $"JOIN fractions " +
            $"ON {_table}.fraction_id = fractions.id " +
            $"WHERE fractions.name = '{fraction}'"
        );

        if (dataTable == null || dataTable.Rows.Count == 0)
            return [];

        List<Card> result = [];

        foreach (System.Data.DataRow row in dataTable.Rows)
        {
            int id = Convert.ToInt32(row["id"]);
            // TODO: SELECT WITH JOIN `card_types`
            //int typeId = Convert.ToBoolean(row["type_id"]);
            bool isHero = Convert.ToBoolean(row["is_hero"]);
            sbyte strength = Convert.ToSByte(row["strength"]);
            Fraction fractionOfCard = Enum.Parse<Fraction>(row["fraction"].ToString()!);
            IEnumerable<FieldLine> fieldLines = JsonSerializer.Deserialize<IEnumerable<FieldLine>>(row["field_lines"].ToString()!)!;
            CardCategory cardCategory = Enum.Parse<CardCategory>(row["card_category"].ToString()!);
            IEnumerable<int>? musterCards = JsonSerializer.Deserialize<IEnumerable<int>>(row["muster_cards"].ToString()!);

            var card = Card.CreateCard(id, strength, fractionOfCard, fieldLines, cardCategory);

            result.Add(card);
        }

        return result;
    }
}
