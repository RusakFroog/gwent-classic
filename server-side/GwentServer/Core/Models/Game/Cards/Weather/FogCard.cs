using Core.Enums.Game;

namespace Core.Models.Game.Cards.Weather;

public class FogCard : Weather
{
    public FogCard() : base("Fog", [FieldLine.Ranger])
    {

    }
}