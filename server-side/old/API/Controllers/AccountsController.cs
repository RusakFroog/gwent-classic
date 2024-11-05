using API.Contracts.Accounts;
using Application.Services;
using Core.Entities.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ExtendedBaseController
{
    private readonly EncryptService _encryptService;
    private readonly AccountsService _accountsService;

    public AccountsController(AccountsService accountsService, EncryptService encryptService)
    {
        _encryptService = encryptService;
        _accountsService = accountsService;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateRequestAccount request)
    {
        try
        {
            // By default "name" is login of account
            var result = await _accountsService.Create(request.Login, request.Login, request.Email, request.Password);

            if (!string.IsNullOrEmpty(result.Error) || result.Value == null)
                return Conflict(result.Error);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, result.Value.Id.ToString())], CookieAuthenticationDefaults.AuthenticationScheme);
        
            await Response.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            HttpContext.Response.Cookies.Append("account_nickname", request.Login, new CookieOptions()
            {
                HttpOnly = false,
                MaxAge = TimeSpan.FromDays(600)
            });
        
            return Created();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
            return BadRequest(ex.ToString());
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestAccount request)
    {
        Account account = await _accountsService.GetAccountByLogin(request.Login);

        if (account == null || !_encryptService.VerifyPassword(request.Password, account.HashedPassword))
            return Conflict("INVALID_DATA");

        ClaimsIdentity claimsIdentity = new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())], CookieAuthenticationDefaults.AuthenticationScheme);
        
        await Response.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        HttpContext.Response.Cookies.Append("account_nickname", account.Name, new CookieOptions()
        {
            HttpOnly = false,
            MaxAge = TimeSpan.FromDays(600)
        });

        return Ok("Logged in");
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await Response.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok("Logged out");
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateRequestAccount request)
    {
        if (string.IsNullOrEmpty(request.Name) || !Regex.IsMatch(request.Name, @"^[a-zA-Z0-9_]+$"))
            return Conflict("Nickname can only contain 'a-Z', '0-9' and '_'");

        if (request.Name.Length > Account.MAX_LENGTH_NAME)
            return BadRequest("Max. length of nick name: " + Account.MAX_LENGTH_NAME);

        var account = await _accountsService.GetAccountById(UserId);

        if (account == null)
            return BadRequest("NO_ACCOUNT_WITH_ID");

        await _accountsService.Update(account.Id, account.Login, request.Name, account.Email, account.HashedPassword, account.Decks);

        Response.Cookies.Append("account_nickname", request.Name, new CookieOptions()
        {
            HttpOnly = false,
            MaxAge = TimeSpan.FromDays(600)
        });
        
        return Ok();
    }
    
    /// <summary>
    /// This route for check if user logged in
    /// </summary>
    /// <returns>status "OK" if user logged in otherwise status "Unauthorized"</returns>
    [Authorize]
    [HttpGet("loggedin")]
    public async Task<IActionResult> LoggedIn()
    {
        var account = await _accountsService.GetAccountById(UserId);

        return account == null ? Unauthorized() : Ok();
    }
}