using Core.Enums.Game;
using Core.Entities.Database;
using Core.Entities.Game.Cards;
using Core.Entities.Game.Cards.Special;
using Core.Entities.Game.Cards.Weather;
using Core.Entities.Game.Cards.Types;
using DataAccess.Databases;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataAccess.Repositories;

public class CardsRepository : Repository<CardEntity>
{
    protected override string _table => "cards";

    private readonly Dictionary<int, Card> _loadedCards;

    public CardsRepository(MySqlDbContext database) : base(database)
    {
        _loadedCards = _getCards().Result;

        _loadCard(Card.CreateCard<KambiCard>(153));
        _loadCard(Card.CreateCard<YoungBerserkCard>(156));
        _loadCard(Card.CreateCard<BerserkCard>(157));
        _loadCard(Card.CreateCard<Ermion>(177));
        _loadCard(Card.CreateCard<MardroemeCard>(181));

        _loadCard(Card.CreateCard<CowCard>(618));

        _loadCard(Card.CreateCard<DecoyCard>(1001));
        _loadCard(Card.CreateCard<HornCard>(1002));
        _loadCard(Card.CreateCard<FogCard>(1003));
        _loadCard(Card.CreateCard<FrostCard>(1004));
        _loadCard(Card.CreateCard<RainCard>(1005));
        _loadCard(Card.CreateCard<SkelligeStormCard>(1006));
        _loadCard(Card.CreateCard<ClearWeatherCard>(1007));
        _loadCard(Card.CreateCard<SpecialScorchCard>(1008));
    }

    public Card GetCardById(int id)
    {
        _loadedCards.TryGetValue(id, out Card value);

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

        Fraction[] fractions = [Fraction.None, Fraction.Monsters, Fraction.Nilfgaardian, Fraction.NorthenRealms, Fraction.Scoiatael, Fraction.Skellige];
        
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
            $"SELECT {_table}.id, {_table}.strength, {_table}.field_lines, {_table}.is_hero, {_table}.muster_cards, fractions.name AS fraction, card_types.name AS type " +
            $"FROM {_table} " +
            $"JOIN fractions ON {_table}.fraction_id = fractions.id " +
            $"JOIN card_types ON {_table}.type_id = card_types.id " +
            $"WHERE fractions.name = '{fraction}'"
        );

        if (dataTable == null || dataTable.Rows.Count == 0)
            return [];

        List<Card> result = [];

        foreach (System.Data.DataRow row in dataTable.Rows)
        {
            int id = Convert.ToInt32(row["id"]);
            bool isHero = Convert.ToBoolean(row["is_hero"]);
            CardType cardType = Enum.Parse<CardType>(row["type"].ToString());
            sbyte strength = Convert.ToSByte(row["strength"]);
            Fraction fractionOfCard = Enum.Parse<Fraction>(row["fraction"].ToString());
            
            IEnumerable<FieldLine> fieldLines = JsonSerializer.Deserialize<IEnumerable<FieldLine>>(row["field_lines"].ToString(), new JsonSerializerOptions
                { Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, true) } }
            );
            IEnumerable<int> musterCards = JsonSerializer.Deserialize<IEnumerable<int>>(row["muster_cards"].ToString());

            var card = Card.CreateCard(id, strength, isHero, fractionOfCard, cardType, fieldLines, musterCards);

            result.Add(card);
        }

        return result;
    }
}
