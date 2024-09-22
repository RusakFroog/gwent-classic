namespace Core.Entities.Game.Cards.Types;

public class MusterCard : Card
{
    public IEnumerable<int> MustersId { get; private set; }

    public MusterCard(int id, sbyte strength, Enums.Game.Fraction fraction, IEnumerable<Enums.Game.FieldLine> lines, Enums.Game.CardCategory cardCategory, IEnumerable<int> mustersId, bool isHero)
        : base(strength, fraction, lines, cardCategory, isHero, id)
    {
        MustersId = mustersId;
    }

    protected override void _onDrop(Player player)
    {
        var findArea = player.CardsInDeck.Concat(player.HandCards).ToList();

        var mustersCards = findArea.FindAll(c => MustersId.Contains(c.Id));

        if (mustersCards.Count == 0)
            return;

        Board board = player.CurrentGame.Board;

        foreach (var c in mustersCards)
            board.AddToLine(c, CurrentField);
    }
}
