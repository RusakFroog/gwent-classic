using Newtonsoft.Json;
using Shared.Models.DataContext;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.Models;

public class AccountEntity
{
    public Guid Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public List<DeckEntity> Decks { get; set; }
}
