using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Special;

public class HornCard : Card
{
    public HornCard() : base("Horn", -1, Fraction.None, [FieldLine.Closer, FieldLine.Ranger, FieldLine.Siege], CardCategory.Special)
    {

    }
}