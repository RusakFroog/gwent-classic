﻿using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Weather;

public class FogCard : Weather
{
    public FogCard() : base(1003, [FieldLine.Ranger])
    {

    }
}