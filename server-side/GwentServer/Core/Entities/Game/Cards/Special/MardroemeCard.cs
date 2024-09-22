namespace Core.Entities.Game.Cards.Special;

public class MardroemeCard : Card
{
    public MardroemeCard() : base(-1, Enums.Game.Fraction.Skellige, [Enums.Game.FieldLine.Ranger], Enums.Game.CardCategory.Special)
    {

    }

    protected override void _onDrop(Player player)
    {
        // TODO: find cards with type Transfromer and replace
    }
}
