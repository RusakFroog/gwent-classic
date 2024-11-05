using Shared.Models.DataContext;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Identity;

public record UpdateAccountControllerRequest
(
    [MaxLength(20)] [MinLength(6, ErrorMessage = "LENGTH_LOGIN")] [RegularExpression(@"^[a-z0-9]+$", ErrorMessage = "ONLY_ALPHANUMERIC_LOGIN")]
    string Login = "",
    [MaxLength(40)] [MinLength(3, ErrorMessage = "LENGTH_NAME")]
    string Name = "",
    [MaxLength(320)] [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "INVALID_EMAIL")]
    string Email = "",
    [MinLength(6, ErrorMessage = "LENGTH_PASSWORD")] [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "ONLY_ALPHANUMERIC_PASSWORD")]
    string Password = "",
    List<DeckEntity> Decks = null
);
