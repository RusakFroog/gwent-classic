using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Weather;

public abstract class Weather : Card
{
    public override bool CanBeTaken => false;
    public override bool HornBoost => false;
    public override bool WeatherImmunity => true;

    protected Weather(string name, IEnumerable<FieldLine> fields, Fraction fraction = Fraction.None) : base(name, -1, fraction, fields)
    {

    }
}