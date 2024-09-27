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
        if (!Enum.TryParse<Fraction>(request.Fraction, true, out var fractionParsed))
            return BadRequest("Invalid fraction");

        string error = await _decksService.UpdateDeck(UserId, fractionParsed, request.Deck);

        if (!string.IsNullOrEmpty(error))
            return BadRequest(error);

        return Ok();
    }

    [Authorize]
    [HttpGet("get/{fraction}")]
    public async Task<IActionResult> GetDecks(string fraction)
    {
        if (!Enum.TryParse<Fraction>(fraction, true, out var fractionParsed))
            return BadRequest("Invalid fraction");

        var resultDeck = await _decksService.GetDeckByFraction(UserId, fractionParsed);

        if (!string.IsNullOrEmpty(resultDeck.Error))
            return BadRequest(resultDeck.Error);

        var resultPool = await _decksService.GetCardsPoolByFraction(UserId, fractionParsed);

        if (!string.IsNullOrEmpty(resultPool.Error))
            return BadRequest(resultPool.Error);

        return Ok(new GetResponseDeck(resultPool.Value, resultDeck.Value));
    }
}