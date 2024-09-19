namespace Core.Entities.Game.Cards.Special;

public class VillentretenmerthCard : Card
{
    public override OnDrop? OnDrop => _onDrop;

    public VillentretenmerthCard() : base(7, Enums.Game.Fraction.None, [Enums.Game.FieldLine.Closer], Enums.Game.CardCategory.None)
    {

    }

    private void _onDrop(Player player)
    {

    }
}
