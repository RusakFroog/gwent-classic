using Game.Models;

namespace API.Contracts.Rooms;

public record GetResponseRoom(IEnumerable<object> Rooms);