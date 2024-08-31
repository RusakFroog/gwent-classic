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

    public async Task<(Room? Room, string Error)> CreateRoom(string id, int userId, string name, string password)
    {
        if (Room.GetRoom(name) != null)
            return new(null, "ROOM_NAME_ALREADY_EXISTS");

        if (Room.Rooms.Values.FirstOrDefault(r => r.OwnerId == userId) != null)
            return new(null, "YOU_ALREADY_HAVE_ROOM");

        Account? account = await _accountsService.GetAccountById(userId);

        if (account == null)
            return new(null, "UNAUTHORIZED");

        Room room = new Room(id, userId, account.Name, name, password);

        return new(room, string.Empty);
    }

    public async Task<string> JoinToRoom(string id, int userId, string password)
    {
        Room? room = Room.GetRoom(id);

        if (room == null)
            return "ROOM_DOES_NOT_EXIST";

        if (room.Password != password)
            return "INCORRECT_PASSWORD";

        Account? account = await _accountsService.GetAccountById(userId);

        if (account == null) 
            return "TO_LOGIN";

        //room.AddPlayer(account);

        return string.Empty;
    }

    public string SetReady(string id, int userId, bool state)
    {
        Room? room = Room.GetRoom(id);
        
        if (room == null)
            return "ROOM_DOES_NOT_EXIST";
        
        //room.SetReady();

        return string.Empty;
    }
    
    public IEnumerable<RoomDTO> GetRoomsChunk(int countLoaded, int count)
    {
        var rooms = Room.Rooms.Values
            .Skip(countLoaded)
            .Take(count);

        List<RoomDTO> result = [];
        
        foreach (Room room in rooms)
        {
            result.Add(
                new RoomDTO(
                    room.OwnerName,
                    !string.IsNullOrEmpty(room.Password), 
                    room.Name, 
                    room.Id
                )
            );
        }
        
        return result;
    }
}
