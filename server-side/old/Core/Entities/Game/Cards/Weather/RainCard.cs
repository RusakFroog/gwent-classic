using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Weather;

public class RainCard : Weather
{
    public RainCard() : base(1005, [FieldLine.Siege])
    {

    }
}