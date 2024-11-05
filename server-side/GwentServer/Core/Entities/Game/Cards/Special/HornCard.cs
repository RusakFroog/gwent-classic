﻿using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Special;

public class HornCard : Card
{
    public HornCard() : base(1002, -1, Fraction.None, [FieldLine.Closer, FieldLine.Ranger, FieldLine.Siege], CardCategory.Special)
    {

    }
}