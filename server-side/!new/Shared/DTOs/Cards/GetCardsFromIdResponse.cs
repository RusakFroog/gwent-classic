using Shared.Models;

namespace Shared.DTOs.Cards;

public record GetCardsFromIdResponse
(
    List<Card> Cards
);
