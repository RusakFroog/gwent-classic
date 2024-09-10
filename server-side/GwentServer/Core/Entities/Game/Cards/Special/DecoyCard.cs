using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Special;

public class DecoyCard : Card
{
    public DecoyCard() : base("Decoy", -1, Fraction.None, [FieldLine.Closer, FieldLine.Ranger, FieldLine.Siege], CardCategory.Special)
    {

    }
}