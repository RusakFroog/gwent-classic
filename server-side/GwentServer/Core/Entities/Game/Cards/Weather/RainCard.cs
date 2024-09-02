using Core.Enums.Game;
using Core.Entities.Game.Cards.Weather;

public class RainCard : Weather
{
    public RainCard() : base("Rain", [FieldLine.Siege])
    {

    }
}