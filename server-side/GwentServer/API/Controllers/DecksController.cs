using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Core.Enums.Game;
using API.Contracts.Decks;
using Application.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DecksController : ControllerBase
{
    private readonly DecksService _decksService;

    public DecksController(DecksService deckService)
    {
        _decksService = deckService;
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateDeck([FromBody] UpdateRequestDeck request)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        string error = await _decksService.UpdateDeck(userId, request.DeckDTO);

        if (!string.IsNullOrEmpty(error))
            return BadRequest(error);

        return Ok();
    }

    [Authorize]
    [HttpGet("get/{fraction}")]
    public async Task<IActionResult> GetDecks(string fraction)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        if (!Enum.TryParse<Fraction>(fraction, true, out var fractionParsed))
            return BadRequest("Invalid fraction");

        var result = await _decksService.GetDeckByFraction(userId, fractionParsed);

        if (!string.IsNullOrEmpty(result.Error))
            return BadRequest(result.Error);

        return Ok(new GetResponseDeck(result.Value!));
    }
}