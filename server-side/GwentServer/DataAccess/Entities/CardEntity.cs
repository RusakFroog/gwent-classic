using Core.Enums.Game;

namespace DataAccess.Entities;

public sealed class CardEntity : EntityBase
{
    public int Strength { get; set; }
    public string Name { get; set; } = string.Empty;
    public Fraction Fraction { get; set; } = Fraction.None;
    public List<FieldLine> FieldLines { get; set; } = [];
    public bool CanBeTaken { get; set; }
    public bool HornBoost { get; set; }
    public bool WeatherImmunity { get; set; }
}