using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Game.Hubs;

[Authorize]
public class GameHub : Hub
{
    /// <summary>
    /// <para>Key - room ID </para>
    /// Value - users ID
    /// </summary>
    private readonly Dictionary<string, List<string>> _rooms = new Dictionary<string, List<string>>();
    
    // public override async Task OnConnectedAsync()
    // {
    //     Guid userId = Guid.Parse(Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    //
    //     Console.WriteLine("[CONNECT] User id: " + userId);
    //     
    //     await base.OnConnectedAsync();
    // }
    
    public async Task CreateGame(string roomId)
    {
        _rooms.Add(roomId, new List<string>());
        
        await JoinToGame(roomId);
    }
    
    public async Task JoinToGame(string roomId)
    {
        if (!_rooms.TryGetValue(roomId, out var roomUsers))
            throw new NullReferenceException($"Room with id: {roomId} wasn't found");

        string userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        if (roomUsers.Contains(userId))
            throw new Exception($"{userId} user already contains");

        roomUsers.Add(userId);
    
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        await Clients.Group(roomId).SendAsync("Client.UserJoined", Context.ConnectionId);
    }
}
