using Core.Entities.Game.Cards.Special;
using Core.Enums.Game;

namespace Core.Entities.Game.Cards;

public delegate void OnDied();
public delegate void OnDrop(Player player);

public class Card
{
    public int Id { get; private set; }
    public sbyte Strength { get; private set; }
    public Fraction Fraction { get; private set; }
    public IEnumerable<FieldLine> Fields { get; private set; }
    public FieldLine CurrentField { get; private set; } = FieldLine.None;   

    public CardCategory CardCategory { get; private set; }

    public virtual OnDied? OnDied { get; private set; } = null;
    public virtual OnDrop? OnDrop { get; private set; } = null;

    public virtual bool IsHero { get; private set; } = false;

    protected Card(sbyte strength, Fraction fraction, IEnumerable<FieldLine> fields, CardCategory cardCategory)
    {
        Strength = strength;
        Fraction = fraction;
        Fields = fields;
        CardCategory = cardCategory;
    }

    private Card(int id, sbyte strength, Fraction fraction, IEnumerable<FieldLine> fields, CardCategory cardCategory)
    {
        Id = id;
        Strength = strength;
        Fraction = fraction;
        Fields = fields;
        CardCategory = cardCategory;
    }

    public static Card CreateCard(int id, sbyte strength, Fraction fraction, IEnumerable<FieldLine> fields, CardCategory cardCategory, List<int>? mustersId = default)
    {
        return new Card(id, strength, fraction, fields, cardCategory);
    }

    public virtual void Apply()
    {

    }

    public static T CreateCard<T>(int id) where T : Card
    {
        var card = Activator.CreateInstance<T>();
        card.Id = id;

        return card;
    }
}
