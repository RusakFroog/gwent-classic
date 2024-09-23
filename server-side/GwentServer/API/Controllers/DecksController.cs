using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Enums.Game;
using API.Contracts.Decks;
using Application.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DecksController : ExtendedBaseController
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
        string error = await _decksService.UpdateDeck(UserId, request.DeckDTO);

        if (!string.IsNullOrEmpty(error))
            return BadRequest(error);

        return Ok();
    }

    [Authorize]
    [HttpGet("get/{fraction}")]
    public async Task<IActionResult> GetDecks(string fraction, CancellationToken ct)
    {
        if (!Enum.TryParse<Fraction>(fraction, true, out var fractionParsed))
            return BadRequest("Invalid fraction");

        var result = await _decksService.GetDeckByFraction(UserId, fractionParsed);

        if (!string.IsNullOrEmpty(result.Error))
            return BadRequest(result.Error);

        return Ok(new GetResponseDeck(result.Value));
    }
}