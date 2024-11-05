using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Cards;

public record GetCardsFromIdRequest
(
    [Required]
    List<int> CardsId
);
