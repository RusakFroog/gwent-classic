using Core.Entities.Game;

namespace API.Contracts.Rooms;

public record GetResponseRoom(IEnumerable<RoomDTO> Rooms);