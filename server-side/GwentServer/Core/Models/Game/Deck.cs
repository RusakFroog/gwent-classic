using Core.Enums.Game;
using Core.Models.Game.Cards;

namespace Core.Models.Game;

public record DeckDTO(Fraction Fraction, IEnumerable<int> CardIds);

public class Deck
{
    public readonly static IReadOnlyList<Deck> Default =
    [
        new Deck(Fraction.None, []),
        new Deck(Fraction.Monsters, []),
        new Deck(Fraction.Nilfgaardian, []),
        new Deck(Fraction.NorthenRealms, []),
        new Deck(Fraction.Scoiatael, []),
        new Deck(Fraction.Skellige, [])
    ];

    public readonly Fraction Fraction;
    public readonly List<Card> Cards;

    public Deck(Fraction fraction, List<Card> cards)
    {
        Fraction = fraction;
        Cards = cards;
    }
}
