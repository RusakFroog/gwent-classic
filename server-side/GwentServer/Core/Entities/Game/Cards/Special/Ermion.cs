namespace Core.Entities.Game.Cards.Special;

public class Ermion : Card
{
    public Ermion() : base(8, Enums.Game.Fraction.Skellige, [Enums.Game.FieldLine.Ranger], Enums.Game.CardCategory.Hero, true)
    {

    }

    protected override void _onDrop(Player player)
    {
        // TODO: find cards with type Transfromer and replace
    }
}
