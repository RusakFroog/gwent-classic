using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Special;

public class ScorchCard : Card
{
    public ScorchCard() : base(-1, Fraction.None, [FieldLine.Closer, FieldLine.Ranger, FieldLine.Siege], CardCategory.Special)
    {
    }
}
