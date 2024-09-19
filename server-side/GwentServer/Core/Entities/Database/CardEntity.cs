using Core.Enums.Game;

namespace Core.Entities.Database;

public sealed class CardEntity : EntityBase
{
    public int Strength { get; set; }
    public string Name { get; set; } = string.Empty;
    public Fraction Fraction { get; set; } = Fraction.None;
    public List<FieldLine> FieldLines { get; set; } = [];
    public CardCategory CardCategory { get; set; } = CardCategory.None;
    public bool IsHero { get; set; }
}