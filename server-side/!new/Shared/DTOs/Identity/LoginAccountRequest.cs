using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Identity;

public record LoginAccountRequest
(
    [Required]
    string Login,
    [Required]
    string Password
);
