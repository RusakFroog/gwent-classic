using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.DTOs.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Shared.Extensions;
using Shared.Exceptions;

namespace IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController(IBusControl busControl) : ExtendedControllerBase
{
    private readonly Uri _identityUrl = new Uri("rabbitmq://localhost/IdentityQueue");

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
    {
        var answer = await _getResponseRabbitTask<CreateAccountRequest, CreateAccountResponse>(_identityUrl, request);

        await _writeCookies(HttpContext, answer.AccountId, answer.AccountName);

        return StatusCode(201);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateAccountControllerRequest request)
    {
        var requestToRabbitMq = new UpdateAccountRequest
        (
            UserId,
            request.Login,
            request.Name,
            request.Email,
            request.Password,
            request.Decks
        );

        await _getResponseRabbitTask<UpdateAccountRequest, EmptyResponse>(_identityUrl, requestToRabbitMq);

        if (!string.IsNullOrEmpty(request.Name))
            Response.Cookies.Append("account_nickname", request.Name, new CookieOptions()
            {
                HttpOnly = false,
                MaxAge = TimeSpan.FromDays(600)
            });

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginAccountRequest request)
    {
        var answer = await _getResponseRabbitTask<LoginAccountRequest, LoginAccountResponse>(_identityUrl, request);

        await _writeCookies(HttpContext, answer.AccountId, answer.AccountName);

        return Ok("Logged in");
    }

    [HttpGet("logged-in")]
    public IActionResult LoggedIn()
    {
        return HttpContext.Items["UserId"] == null 
            ? throw new UnauthorizationException("User is unauthorized") 
            : Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        if (HttpContext.Items["UserId"] == null)
            throw new UnauthorizationException("User is unauthorized");

        await Response.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok("Logged out");
    }

    private static async Task _writeCookies(HttpContext httpContext, Guid accountId, string accountName)
    {
        var claimsIdentity = new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, accountId.ToString())], CookieAuthenticationDefaults.AuthenticationScheme);

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        httpContext.Response.Cookies.Append("account_nickname", accountName, new CookieOptions
        {
            HttpOnly = false,
            MaxAge = TimeSpan.FromDays(600)
        });
    }

    private async Task<TOut> _getResponseRabbitTask<TIn, TOut>(Uri uri, TIn request) 
        where TIn : class 
        where TOut : class
    {
        var client = busControl.CreateRequestClient<TIn>(uri);
        var response = await client.GetResponse<TOut>(request);

        return response.Message;
    }
}
