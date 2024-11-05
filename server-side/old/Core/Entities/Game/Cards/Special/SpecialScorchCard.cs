using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Special;

public class SpecialScorchCard : Card
{
    public SpecialScorchCard() : base(1008, -1, Fraction.None, [FieldLine.Closer, FieldLine.Ranger, FieldLine.Siege], CardCategory.Special)
    {
    }
}
