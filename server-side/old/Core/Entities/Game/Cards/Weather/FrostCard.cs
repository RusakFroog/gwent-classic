using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Weather;

public class FrostCard : Weather
{
    public FrostCard() : base(1004, [FieldLine.Closer])
    {

    }
}