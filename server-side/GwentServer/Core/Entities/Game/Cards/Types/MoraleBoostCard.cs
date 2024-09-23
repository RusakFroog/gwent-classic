namespace Core.Entities.Game.Cards.Types;

public class MoraleBoostCard : Card
{
    public MoraleBoostCard(int id, sbyte strength, Enums.Game.Fraction fraction, IEnumerable<Enums.Game.FieldLine> lines, Enums.Game.CardCategory cardCategory, bool isHero)
        : base(id, strength, fraction, lines, cardCategory, isHero)
    {

    }

    protected override void _onDrop(Player player)
    {

    }
}
