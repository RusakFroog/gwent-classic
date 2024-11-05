using Shared.Models;

namespace Shared.DTOs.Identity;

public record CreateAccountResponse(Guid AccountId, string AccountName);