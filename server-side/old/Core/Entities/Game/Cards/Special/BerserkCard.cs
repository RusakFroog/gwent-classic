using Core.Entities.Game.Cards.Types;

namespace Core.Entities.Game.Cards.Special;

public class BerserkCard : TransformerCard
{
    public BerserkCard() : base(157, 4, Enums.Game.Fraction.Skellige, [Enums.Game.FieldLine.Closer], Enums.Game.CardCategory.None, false, 176)
    {

    }
}
