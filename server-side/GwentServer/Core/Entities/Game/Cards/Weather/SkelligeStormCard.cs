using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Weather;

public class SkelligeStormCard : Weather
{
    public SkelligeStormCard() : base("Skellige Storm", [FieldLine.Siege, FieldLine.Ranger], Fraction.Skellige)
    {

    }
}