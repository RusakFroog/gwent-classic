namespace Core.Entities.Game.Cards.Special;

public class KambiCard : Card
{
    public KambiCard() : base(0, Enums.Game.Fraction.Skellige, [Enums.Game.FieldLine.Closer], Enums.Game.CardCategory.None)
    {

    }

    protected override void _onDied()
    {
        Card.CreateCard<HemdallCard>(180);
        //.. drop on board
    }
}
