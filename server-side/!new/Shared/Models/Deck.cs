using Shared.Enums;

namespace Shared.Models;

public class Deck
{
    public static readonly List<Deck> Default =
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