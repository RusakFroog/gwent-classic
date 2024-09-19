namespace Core.Entities.Game.Cards.Special;

public class MusterCard : Card
{
    public override OnDrop? OnDrop => _onDrop;

    public IEnumerable<int> MustersId { get; private set; }

    public MusterCard(sbyte strength, Enums.Game.Fraction fraction, IEnumerable<Enums.Game.FieldLine> lines, Enums.Game.CardCategory cardCategory, IEnumerable<int> mustersId) 
        : base(strength, fraction, lines, cardCategory) 
    {
        MustersId = mustersId;
    }

    private void _onDrop(Player player)
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
