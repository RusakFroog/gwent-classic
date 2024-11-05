using Core.Enums.Game;
using Core.Entities.Game.Cards;

namespace Core.Entities.Game;

public record CardDTO(int Id, IEnumerable<string> Lines, string Category);
public record DeckDTO(Fraction Fraction, IEnumerable<CardDTO> Cards);

public class Deck
{
    public readonly static IReadOnlyList<Deck> Default =
    [
        new Deck(Fraction.None, []),
        new Deck(Fraction.Monsters, []),
        new Deck(Fraction.Nilfgaardian, []),
        new Deck(Fraction.NorthernRealms, []),
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
