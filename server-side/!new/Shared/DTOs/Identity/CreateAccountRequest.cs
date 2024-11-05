using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Identity;

public record CreateAccountRequest
(
    [Required] [MaxLength(20)] [MinLength(6, ErrorMessage = "LENGTH_LOGIN")] [RegularExpression(@"^[a-z0-9]+$", ErrorMessage = "ONLY_ALPHANUMERIC_LOGIN")]
    string Login, 
    [Required] [MaxLength(320)] [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "INVALID_EMAIL")]
    string Email,
    [Required] [MinLength(6, ErrorMessage = "LENGTH_PASSWORD")] [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "ONLY_ALPHANUMERIC_PASSWORD")]
    string Password
);
