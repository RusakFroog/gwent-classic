using Core.Entities.Game.Cards.Types;
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

    public OnDied OnDied { get; private set; }
    public OnDrop OnDrop { get; private set; }

    public virtual bool IsHero { get; private set; } = false;

    protected Card(int id, sbyte strength, Fraction fraction, IEnumerable<FieldLine> fields, CardCategory cardCategory = CardCategory.None, bool isHero = false)
    {
        Id = id;
        Strength = strength;
        Fraction = fraction;
        Fields = fields;
        IsHero = isHero;
        CardCategory = cardCategory == CardCategory.None ?
            (isHero ? CardCategory.Hero : CardCategory.None) : cardCategory;

        OnDied += _onDied;
        OnDrop += _onDrop;
    }

    public static Card CreateCard(int id, sbyte strength, bool isHero, Fraction fraction, CardType cardType, IEnumerable<FieldLine> fields, IEnumerable<int> mustersId = default, CardCategory cardCategory = CardCategory.None)
    {
        return cardType switch
        {
            CardType.Medic => new MedicCard(id, strength, fraction, fields, cardCategory, isHero),
            CardType.Spy => new SpyCard(id, strength, fraction, fields, cardCategory, isHero),
            CardType.Muster => new MusterCard(id, strength, fraction, fields, cardCategory, mustersId, isHero),
            CardType.TightBond => new TightBondCard(id, strength, fraction, fields, cardCategory, isHero),
            CardType.MoraleBoost => new MoraleBoostCard(id, strength, fraction, fields, cardCategory, isHero),
            CardType.Scorch => new ScorchCard(id, strength, fraction, fields, cardCategory, isHero),
            CardType.CommanderHorn => new CommanderHornCard(id, strength, fraction, fields, cardCategory, isHero),
            _ => new Card(id, strength, fraction, fields, cardCategory, isHero)
        };
    }

    public virtual void Apply()
    {

    }

    protected virtual void _onDrop(Player player)
    {

    }

    protected virtual void _onDied()
    {

    }
}
