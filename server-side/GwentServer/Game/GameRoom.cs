using Microsoft.AspNetCore.SignalR;

namespace Game;

public class GameRoom
{
    public static readonly Dictionary<string, GameRoom> Rooms = [];

    public readonly string OwnerId;
    public readonly string Id;
    public readonly string Name;
    public readonly string Password;
    public readonly List<string> Users = [];
    
    private GameRoom(string id, string ownerId, string name, string password)
    {
        OwnerId = ownerId;
        Id = id;
        Name = name;
        Password = password;
        
        Rooms.Add(id, this);
    }

    public static GameRoom CreateRoom(string id, string ownerId, string roomName, string password = "")
    {
        return new GameRoom(id, ownerId, roomName, password);
    }

    public async Task<bool> AddUser(string userId, Hub hub)
    {
        bool userContains = Users.Contains(userId);
        
        if (!userContains)
        {
            Users.Add(userId);
            
            await hub.Groups.AddToGroupAsync(userId, Id);
        }

        return !userContains;
    }

    public async Task RemoveUser(string userId, Hub hub)
    {
        if (Users.Contains(userId))
        {
            Users.Remove(userId);
            
            await hub.Groups.RemoveFromGroupAsync(userId, Id);
        }
    }

    public async Task InvokeEvent(string eventName, Hub hub, params object[] options)
    {
        await hub.Clients.Group(Id).SendAsync(eventName, options);
    }

    public async Task DeleteRoom(Hub hub)
    {
        foreach (string userId in Users)
            await RemoveUser(userId, hub);
        
        Rooms.Remove(Id);
    }
}
