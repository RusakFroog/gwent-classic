using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Weather;

public abstract class Weather : Card
{
    protected Weather(IEnumerable<FieldLine> fields, Fraction fraction = Fraction.None) : base(-1, fraction, fields, CardCategory.Weather)
    {

    }
}