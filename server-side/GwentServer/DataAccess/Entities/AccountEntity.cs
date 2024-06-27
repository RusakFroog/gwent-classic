using Core.Enums.Game;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("accounts")]
public sealed class AccountEntity
{
    public Guid Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Dictionary with fractions as keys and list of card ids as values
    /// </summary>
    public Dictionary<Fraction, List<int>> Decks { get; set; } = [];
}