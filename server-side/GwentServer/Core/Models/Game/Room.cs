using Core.Models.Game.Players;

namespace Core.Models.Game;

public record RoomDTO(string? Owner, bool Password, string Name, string Uuid);

public class Room
{
    public static Dictionary<string, Room> Rooms = new Dictionary<string, Room>();

    public readonly string Id;
    public readonly string Name;
    public readonly string Password;
    public readonly string OwnerId;
    
    private PlayerReadiness _playerReadiness { get; set; } = new PlayerReadiness(false, false);

    private Player? _firstPlayer { get; set; }
    private Player? _secondPlayer { get; set; }
    
    public Room(string uuid, string ownerUserId, string name, string password)
    {
        Id = uuid;
        Name = name;
        Password = password;
        OwnerId = ownerUserId;
        
        Rooms.Add(uuid, this);
    }

    public static Room? GetRoom(string id)
    {
        return Rooms.Values.FirstOrDefault(r => r.Id == id);
    }

    public void StartGame()
    {
        ArgumentNullException.ThrowIfNull(_firstPlayer);
        ArgumentNullException.ThrowIfNull(_secondPlayer);
        
        new Game(_firstPlayer, _secondPlayer);

        RemoveRoom();
    }

    public void SetReady(string userId)
    {
        if (_firstPlayer == null)
        {
            RemoveRoom();
            return;
        }
        
        if (_firstPlayer.UserId == userId)
            _playerReadiness.FirstPlayer = true;
        else
            _playerReadiness.SecondPlayer = true;
    }
    
    public void AddPlayer()
    {
        // if (_firstPlayer == null)
        // {
        //     _firstPlayer = new Player();
        // }
        // else
        // {
        //     _secondPlayer = new Player();
        // }
    }

    public void RemoveRoom()
    {
        Rooms.Remove(Id);
    }
}