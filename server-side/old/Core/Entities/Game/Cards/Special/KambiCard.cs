namespace Core.Entities.Game.Cards.Special;

public class KambiCard : Card
{
    public KambiCard() : base(153, 0, Enums.Game.Fraction.Skellige, [Enums.Game.FieldLine.Closer], Enums.Game.CardCategory.None)
    {

    }

    protected override void _onDied()
    {
        new HemdallCard();
        //.. drop on board
    }
}
