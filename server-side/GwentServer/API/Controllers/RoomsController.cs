using API.Contracts.Rooms;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [Authorize]
    [HttpPost("create")]
    public IActionResult CreateRoom([FromBody] CreateRequestRoom request)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = _roomsService.CreateRoom(request.Id, userId, request.Name, request.Password);

        if (!string.IsNullOrEmpty(result.Error))
            return BadRequest(result.Error);
        
        return Ok();
    }

    [Authorize]
    [HttpPost("join/{id}")]
    public async Task<IActionResult> JoinToRoom(string id, [FromBody] string password)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        string error = await _roomsService.JoinToRoom(id, userId, password);
        
        if (!string.IsNullOrEmpty(error))
            return BadRequest(error);

        return Ok();
    }

    [Authorize]
    [HttpPost("setready/{id}")]
    public IActionResult SetReady(string id, [FromQuery] bool state)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        string error = _roomsService.SetReady(id, userId, state);
        
        if (!string.IsNullOrEmpty(error))
            return BadRequest(error);
        
        return Ok();
    }

    [HttpGet("getrooms")]
    public async Task<IActionResult> GetRooms([FromQuery] GetRequestRoom request)
    {
        var rooms = await _roomsService.GetRoomsChunk(request.LoadedCount, request.NeedLoad);
        
        return Ok(new GetResponseRoom(rooms));
    }
}