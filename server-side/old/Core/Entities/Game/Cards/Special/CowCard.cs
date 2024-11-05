using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Special;

public class CowCard : Card
{
    public CowCard() : base(700, 0, Fraction.None, [FieldLine.Ranger], CardCategory.None)
    {

    }

    protected override void _onDied()
    {
        new BovineDefenseForceCard();
        //.. drop on board
    }
}
