using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Weather;

public class SkelligeStormCard : Weather
{
    public SkelligeStormCard() : base(1006, [FieldLine.Siege, FieldLine.Ranger], Fraction.None)
    {

    }
}