using Core.Entities.Game.Cards.Types;

namespace Core.Entities.Game.Cards.Special;

public class TransformedVildkaarlCard : MoraleBoostCard
{
    public TransformedVildkaarlCard() : base(175, 14, Enums.Game.Fraction.Skellige, [Enums.Game.FieldLine.Closer], Enums.Game.CardCategory.None, false)
    {

    }
}
