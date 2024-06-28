using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Services;
using Core.Enums.Game;
using API.Contracts.Decks;

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
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        string error = await _decksService.UpdateDeck(userId, request.DeckDTO);

        if (!string.IsNullOrEmpty(error))
            return BadRequest(error);

        return Ok();
    }

    [Authorize]
    [HttpGet("get/{fraction}")]
    public async Task<IActionResult> GetDecks(string fraction)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        if (!Enum.TryParse<Fraction>(fraction, true, out var fractionParsed))
            return BadRequest("Invalid fraction");

        var deck = await _decksService.GetDeckByFraction(userId, fractionParsed);

        return Ok(new GetResponseDeck(deck));
    }
}