using Core.Enums.Game;
using Core.Models.Game.Cards.Weather;

public class RainCard : Weather
{
    public RainCard() : base("Rain", [FieldLine.Siege])
    {

    }
}