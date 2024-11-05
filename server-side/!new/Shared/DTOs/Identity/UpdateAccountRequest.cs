using Shared.Models.DataContext;

namespace Shared.DTOs.Identity;

public record UpdateAccountRequest
(
    Guid Id,
    string Login = "",
    string Name = "",
    string Email = "",
    string Password = "",
    List<DeckEntity> Decks = null
);