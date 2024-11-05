using Shared.Enums;

namespace Shared.Models.DataContext;

public class DeckEntity
{
    public static readonly List<DeckEntity> Default =
    [
        new DeckEntity() { Fraction = Fraction.Monsters, CardsId = [] },
        new DeckEntity() { Fraction = Fraction.Nilfgaardian, CardsId = [] },
        new DeckEntity() { Fraction = Fraction.NorthernRealms, CardsId = [] },
        new DeckEntity() { Fraction = Fraction.Scoiatael, CardsId = [] },
        new DeckEntity() { Fraction = Fraction.Skellige, CardsId = [] }
    ];

    public Fraction Fraction { get; set; }
    public List<int> CardsId { get; set; }
}
