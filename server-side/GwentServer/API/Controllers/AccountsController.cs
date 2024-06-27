using API.Contracts.Accounts;
using Application.Services;
using Core.Models;
using DataAccess.DbContexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly AccountsService _accountsService;

    public AccountsController(AccountsService accountsService)
    {
        _accountsService = accountsService;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateRequestAccount request)
    {
        // By default "name" is login of account
        var result = await _accountsService.Create(request.Login, request.Login, request.Email, request.Password);

        if (!string.IsNullOrEmpty(result.Item2))
            return Conflict(result.Item2);

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, result.Item1!.Id.ToString()) }, CookieAuthenticationDefaults.AuthenticationScheme);
        
        await Response.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        HttpContext.Response.Cookies.Append("account_nickname", request.Login, new CookieOptions()
        {
            HttpOnly = false,
            MaxAge = TimeSpan.FromDays(600)
        });
        
        return Created();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestAccount request)
    {
        Account? account = await _accountsService.GetAccountByLogin(request.Login);

        if (account == null || !account.VerifyPassword(request.Password))
            return Conflict("INVALID_DATA");

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()) }, CookieAuthenticationDefaults.AuthenticationScheme);
        
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

        Guid userId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        await _accountsService.Update(userId, string.Empty, request.Name, string.Empty, null);

        HttpContext.Response.Cookies.Append("account_nickname", request.Name, new CookieOptions()
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
    public IActionResult LoggedIn()
    {
        return Ok();
    }
}