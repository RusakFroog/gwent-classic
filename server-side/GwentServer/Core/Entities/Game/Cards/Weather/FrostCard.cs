using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Weather;

public class FrostCard : Weather
{
    public FrostCard() : base("Frost", [FieldLine.Closer])
    {

    }
}