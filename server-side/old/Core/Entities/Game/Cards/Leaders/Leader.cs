using Core.Enums.Game;

namespace Core.Entities.Game.Cards.Leaders;

public abstract class Leader
{
    /// <summary>
    /// Id of image from CDN
    /// </summary>
    public int Id { get; private set; }
    public string SpecialAbility { get; private set; }
    public Fraction Fraction { get; private set; }

    public Leader(int id, string specialAbility, Fraction fraction)
    {
        Id = id;
        SpecialAbility = specialAbility;
        Fraction = fraction;
    }

    public abstract void OnDrop();
}
