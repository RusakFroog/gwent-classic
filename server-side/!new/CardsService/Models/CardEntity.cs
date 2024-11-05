using Shared.Enums;

namespace CardsService.Models;

public class CardEntity
{
    public int Id { get; set; }
    public sbyte Strength { get; set; }
    public string Name { get; set; } = string.Empty;
    public Fraction Fraction { get; set; } = Fraction.None;
    public List<FieldLine> FieldLines { get; set; } = [];
    public CardCategory CardCategory { get; set; } = CardCategory.None;
    public CardType CardType { get; set; } = CardType.None;
    public bool IsHero { get; set; } = false;
}