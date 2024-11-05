using Shared.Enums;

namespace Shared.Models;

public delegate void OnDied();
public delegate void OnDrop(Player player);

public class Card
{
    public int Id { get; private set; }
    public sbyte Strength { get; private set; }
    public Fraction Fraction { get; private set; }
    public CardCategory CardCategory { get; private set; }

    public IEnumerable<FieldLine> Fields { get; private set; }
    public FieldLine CurrentField { get; private set; } = FieldLine.None;

    public OnDied OnDied { get; private set; }
    public OnDrop OnDrop { get; private set; }

    public bool IsHero { get; private set; }

    public Card()
    {

    }

    public Card(int id, sbyte strength, Fraction fraction, CardCategory cardCategory, IEnumerable<FieldLine> fieldLines, bool isHero)
    {
        Id = id;
        Strength = strength;
        Fraction = fraction;
        CardCategory = cardCategory;
        Fields = fieldLines;
        IsHero = isHero;
    }
}