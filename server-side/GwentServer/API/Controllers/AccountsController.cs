using API.Contracts.Accounts;
using Application.Services;
using Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        string error = await _accountsService.Create(request.Login, request.Email, request.Password);

        if (!string.IsNullOrEmpty(error))
            return Conflict(error);

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, request.Login) }, CookieAuthenticationDefaults.AuthenticationScheme);
        
        await Response.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        
        return Created();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestAccount request)
    {
        Account? account = await _accountsService.GetAccountByLogin(request.Login);

        if (account == null || !account.VerifyPassword(request.Password))
            return Conflict("INVALID_DATA");
        
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, request.Login) }, CookieAuthenticationDefaults.AuthenticationScheme);
        
        await Response.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        
        return Ok("Logged in");
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await Response.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return Ok("Logged out");
    }
}