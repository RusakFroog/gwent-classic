using Shared.Models.DataContext;
using Shared.DTOs;
using Shared.Models;
using Shared.DTOs.Identity;

namespace IdentityService.Services;

public interface IAccountService
{
    Task<CreateAccountResponse> Create(string login, string name, string email, string password);
    Task<EmptyResponse> Update(Guid id, string login, string name, string email, string password, List<DeckEntity> decks);
    Task<LoginAccountResponse> Login(string login, string password);
    Task<Account> GetById(Guid id);
}
