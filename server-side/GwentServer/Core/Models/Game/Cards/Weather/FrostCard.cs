using Core.Enums.Game;

namespace Core.Models.Game.Cards.Weather;

public class FrostCard : Weather
{
    public FrostCard() : base("Frost", [FieldLine.Closer])
    {

    }
}