namespace Core.Entities.Game.Cards.Special;

public class MoraleBoostCard : Card
{
    public override OnDrop? OnDrop => _onDrop;

    public MoraleBoostCard(sbyte strength, Enums.Game.Fraction fraction, IEnumerable<Enums.Game.FieldLine> lines, Enums.Game.CardCategory cardCategory)
        : base(strength, fraction, lines, cardCategory)
    {

    }

    private void _onDrop(Player player)
    {

    }
}
