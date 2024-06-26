using Core.Enums.Game;
using Core.Models.Game.Cards;

namespace Core.Models.Game.Decks;

public record DeckDTO(Fraction Fraction, IEnumerable<int> CardIds);

public class Deck
{
    public readonly static IReadOnlyList<Deck> Default = 
    [
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

    public static Dictionary<Fraction, List<int>> ConvertToIds(IEnumerable<Deck> decks)
    {
        Dictionary<Fraction, List<int>> result = [];

        foreach (var deck in decks)
        {
            if (result.ContainsKey(deck.Fraction))
                result[deck.Fraction].AddRange(deck.Cards.Select(x => x.Id));
            else
                result.Add(deck.Fraction, deck.Cards.Select(x => x.Id).ToList());
        }

        return result;
    }
}
