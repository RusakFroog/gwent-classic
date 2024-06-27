using Core.Enums.Game;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("cards")]
public class CardEntity
{
    public int Id { get; set; }
    public int Strength { get; set; }
    public string Name { get; set; } = string.Empty;
    public Fraction Fraction { get; set; } = Fraction.None;
    public List<FieldLine> FieldLines { get; set; } = [];
    public bool CanBeTaken { get; set; }
    public bool HornBoost { get; set; }
    public bool WeatherImmunity { get; set; }
}