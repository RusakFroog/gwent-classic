using Game.Models;

namespace Application.Services;

public class RoomsService
{
    private readonly AccountsService _accountsService;
    
    public RoomsService(AccountsService accountsService)
    {
        _accountsService = accountsService;
    }

    public async Task<IEnumerable<object>> GetRoomsChunk(int countLoaded, int count)
    {
        var rooms = GameRoom.Rooms.Values
            .Skip(countLoaded)
            .Take(count);

        List<object> result = new List<object>();
        
        foreach (GameRoom room in rooms)
        {
            var account = await _accountsService.GetAccountById(Guid.Parse(room.OwnerId));
            
            result.Add(new
            {
                owner = account?.Name,
                password = !string.IsNullOrEmpty(room.Password),
                name = room.Name
            });
        }
        
        return result;
    }
}
