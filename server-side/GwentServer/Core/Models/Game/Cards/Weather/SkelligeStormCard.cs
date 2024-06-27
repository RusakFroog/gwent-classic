using Core.Enums.Game;

namespace Core.Models.Game.Cards.Weather;

public class SkelligeStormCard : Weather
{
    public SkelligeStormCard() : base("Skellige Storm", [FieldLine.Siege, FieldLine.Ranger], Fraction.Skellige)
    {

    }
}