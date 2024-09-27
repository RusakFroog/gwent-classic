using Core.Enums.Game;

namespace API.Contracts.Decks;

public record UpdateRequestDeck(string Fraction, IEnumerable<int> Deck);