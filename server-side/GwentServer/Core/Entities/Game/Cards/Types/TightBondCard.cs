namespace Core.Entities.Game.Cards.Types;

public class TightBondCard : Card
{
    public TightBondCard(int id, sbyte strength, Enums.Game.Fraction fraction, IEnumerable<Enums.Game.FieldLine> lines, Enums.Game.CardCategory cardCategory, bool isHero)
        : base(strength, fraction, lines, cardCategory, isHero, id)
    {

    }
}
