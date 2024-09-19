using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Weather;

public class ClearWeatherCard : Card
{
    public ClearWeatherCard() : base(-1, Fraction.None, [FieldLine.Closer, FieldLine.Ranger, FieldLine.Siege], CardCategory.Weather)
    {

    }
}
