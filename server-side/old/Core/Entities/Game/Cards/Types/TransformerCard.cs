namespace Core.Entities.Game.Cards.Types;

public class TransformerCard : Card
{
    public int TransfromIntoId { get; private set; }

    public TransformerCard(int id, sbyte strength, Enums.Game.Fraction fraction, IEnumerable<Enums.Game.FieldLine> lines, Enums.Game.CardCategory cardCategory, bool isHero, int transfromIntoId)
        : base(id, strength, fraction, lines, cardCategory, isHero)
    {
        TransfromIntoId = transfromIntoId;
    }
}
