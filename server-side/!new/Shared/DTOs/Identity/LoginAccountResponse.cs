namespace Shared.DTOs.Identity;

public record LoginAccountResponse
(
    Guid AccountId,
    string AccountName
);