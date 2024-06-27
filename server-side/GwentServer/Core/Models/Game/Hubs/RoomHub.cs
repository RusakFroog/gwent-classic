using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Core.Models.Game.Hubs;

[Authorize]
public class GameHub : Hub
{
    public async Task Create(string uuid, string roomName, string roomPassword)
    {
        // new Room()
    }
}
