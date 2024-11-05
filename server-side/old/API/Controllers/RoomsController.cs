using API.Contracts.Rooms;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ExtendedBaseController
{
    private readonly RoomsService _roomsService;
    
    public RoomsController(RoomsService roomsService)
    {
        _roomsService = roomsService;
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRequestRoom request)
    {
        var result = await _roomsService.CreateRoom(request.Id, UserId, request.Name, request.Password);

        if (!string.IsNullOrEmpty(result.Error))
            return BadRequest(result.Error);
        
        return Ok();
    }

    [Authorize]
    [HttpPost("join/{id}")]
    public async Task<IActionResult> JoinToRoom(string id, [FromBody] JoinRequestRoom request)
    {
        string error = await _roomsService.JoinToRoom(id, UserId, request.Password);
        
        if (!string.IsNullOrEmpty(error))
            return BadRequest(error);

        return Ok();
    }
    
    [Authorize]
    [HttpPost("setready/{id}")]
    public IActionResult SetReady(string id, [FromQuery] bool state)
    {
        string error = _roomsService.SetReady(id, UserId, state);
        
        if (!string.IsNullOrEmpty(error))
            return BadRequest(error);
        
        return Ok();
    }

    [HttpGet("getrooms")]
    public IActionResult GetRooms([FromQuery] GetRequestRoom request)
    {
        var rooms = _roomsService.GetRoomsChunk(request.LoadedCount, request.NeedLoad);
        
        return Ok(new GetResponseRoom(rooms));
    }
}