using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Weather;

public abstract class Weather : Card
{
    protected Weather(int id, IEnumerable<FieldLine> fields, Fraction fraction = Fraction.None) : base(id, -1, fraction, fields, CardCategory.Weather)
    {

    }
}