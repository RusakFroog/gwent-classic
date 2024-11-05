using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Cards;

public record GetIdsFromCardRequest
(
    [Required]
    List<Card> Cards
);