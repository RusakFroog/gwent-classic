using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Game.Hubs;

[Authorize]
public class GameHub : Hub
{
    public async Task<bool> CreateRoom(string roomId, string roomName, string password)
    {
        string userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        if (GameRoom.Rooms.Values.FirstOrDefault(r => r.Name == roomName) != null)
        {
            await Clients.Caller.SendAsync("Client.SendError", "Room with this name already created");
            return false;
        }
        
        GameRoom.CreateRoom(roomId, userId, roomName, password);
        
        await JoinToRoom(roomId, password);

        return true;
    }
    
    public async Task RemoveRoom(string roomId)
    {
        if (!GameRoom.Rooms.TryGetValue(roomId, out var gameRoom))
        {
            await Clients.Caller.SendAsync("Client.SendError", "Room doesn't exist");
            return;
        }

        await gameRoom.DeleteRoom(this);
    }
    
    public async Task<bool> JoinToRoom(string roomId, string password)
    {
        GameRoom? gameRoom;
        
        if (!GameRoom.Rooms.TryGetValue(roomId, out gameRoom))
        {
            gameRoom = GameRoom.Rooms.Values.FirstOrDefault(r => r.Name == roomId);
            
            if (gameRoom == null)
            {
                await Clients.Caller.SendAsync("Client.SendError", "Room doesn't exist");
                return false;
            }
        }
        
        if (!string.IsNullOrEmpty(gameRoom.Password) && gameRoom.Password != password.Trim())
        {
            await Clients.Caller.SendAsync("Client.SendError", "Wrong password");
            return false;
        }

        string userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        bool userAdded = await gameRoom.AddUser(userId, this);
        
        if (!userAdded)
        {
            await Clients.Caller.SendAsync("Client.SendError", "User already in room");
            return false;
        }
        
        await gameRoom.InvokeEvent("Client.JoinUser", this, userId);
        
        return true;
    }
}
