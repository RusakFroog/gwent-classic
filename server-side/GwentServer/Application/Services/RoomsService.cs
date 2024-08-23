using Core.Models.Account;
using Core.Models.Game;

namespace Application.Services;

public class RoomsService
{
    private readonly AccountsService _accountsService;
    
    public RoomsService(AccountsService accountsService)
    {
        _accountsService = accountsService;
    }

    public (Room? Room, string Error) CreateRoom(string id, int userId, string name, string password)
    {
        if (Room.GetRoom(name) != null)
            return new(null, "Room with this name already exist");

        if (Room.Rooms.Values.FirstOrDefault(r => r.OwnerId == userId) != null)
            return new(null, "You already have created room");
        
        return new(new Room(id, userId, name, password), string.Empty);
    }

    public async Task<string> JoinToRoom(string id, int userId, string password)
    {
        Room? room = Room.GetRoom(id);

        if (room == null)
            return "Room doesn't exist";

        if (room.Password != password)
            return "Password isn't correct";

        Account? account = await _accountsService.GetAccountById(userId);

        if (account == null) 
            return "TO_LOGIN";

        //room.AddPlayer(account);

        return string.Empty;
    }

    public string SetReady(string id, string userId, bool state)
    {
        Room? room = Room.GetRoom(id);
        
        if (room == null)
            return "Room doesn't exist";
        
        //room.SetReady();

        return string.Empty;
    }
    
    public async Task<IEnumerable<RoomDTO>> GetRoomsChunk(int countLoaded, int count)
    {
        var rooms = Room.Rooms.Values
            .Skip(countLoaded)
            .Take(count);

        List<RoomDTO> result = [];
        
        foreach (Room room in rooms)
        {
            var account = await _accountsService.GetAccountById(room.OwnerId);

            if (account == null) 
                continue;

            result.Add(
                new RoomDTO(
                    account.Name,
                    !string.IsNullOrEmpty(room.Password), 
                    room.Name, 
                    room.Id
                )
            );
        }
        
        return result;
    }
}
