using Core.Entities.Game;

namespace API.Contracts.Decks;

public record GetResponseDeck(DeckDTO PoolCards, DeckDTO DeckCards);