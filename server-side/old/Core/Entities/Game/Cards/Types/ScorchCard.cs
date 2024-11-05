namespace Core.Entities.Game.Cards.Types;

public class ScorchCard : Card
{
    public ScorchCard(int id, sbyte strength, Enums.Game.Fraction fraction, IEnumerable<Enums.Game.FieldLine> lines, Enums.Game.CardCategory cardCategory, bool isHero)
        : base(id, strength, fraction, lines, cardCategory, isHero)
    {

    }
}
