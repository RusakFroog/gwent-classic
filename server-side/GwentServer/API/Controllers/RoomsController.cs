using API.Contracts.Rooms;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly RoomsService _roomsService;
    
    public RoomsController(RoomsService roomsService)
    {
        _roomsService = roomsService;
    }

    [HttpGet("getrooms")]
    public async Task<IActionResult> GetRooms([FromQuery] GetRequestRoom request)
    {
        var rooms = await _roomsService.GetRoomsChunk(request.LoadedCount, request.NeedLoad);
        
        return Ok(new GetResponseRoom(rooms));
    }
}