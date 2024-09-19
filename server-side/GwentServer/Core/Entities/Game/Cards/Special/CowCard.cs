using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Special;

public class CowCard : Card
{
    public override OnDied OnDied => _onDie;

    public CowCard() : base(0, Fraction.None, [FieldLine.Ranger], CardCategory.None)
    {

    }

    private void _onDie()
    {
        Card.CreateCard<BovineDefenseForceCard>(620);
    }
}
