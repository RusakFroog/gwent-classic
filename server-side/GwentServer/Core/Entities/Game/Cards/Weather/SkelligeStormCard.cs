using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Weather;

public class SkelligeStormCard : Weather
{
    public SkelligeStormCard() : base([FieldLine.Siege, FieldLine.Ranger], Fraction.None)
    {

    }
}