namespace Core.Entities.Game.Cards.Types;

public class CommanderHornCard : Card
{
    public CommanderHornCard(int id, sbyte strength, Enums.Game.Fraction fraction, IEnumerable<Enums.Game.FieldLine> lines, Enums.Game.CardCategory cardCategory, bool isHero)
        : base(strength, fraction, lines, cardCategory, isHero, id)
    {

    }
}
