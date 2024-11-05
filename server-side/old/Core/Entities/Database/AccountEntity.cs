using Core.Enums.Game;

namespace Core.Entities.Database;

public sealed class AccountEntity : EntityBase
{
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;

    /// <summary>
    /// Dictionary with fractions as keys and list of card ids as values
    /// </summary>
    public Dictionary<Fraction, List<int>> Decks { get; set; } = [];
}