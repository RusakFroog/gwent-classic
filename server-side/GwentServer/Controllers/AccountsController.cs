using GwentServer.Contracts.Accounts;
using GwentServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace GwentServer.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly AccountsService _accountsService;

    public AccountsController(AccountsService accountsService)
    {
        _accountsService = accountsService;
    }
    
    [Route("create")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequestAccount request)
    {
        string error = await _accountsService.Create(request.Login, request.Email, request.Password);

        if (!string.IsNullOrEmpty(error))
            return Conflict(error);
        
        return Created(string.Empty, "Account successfully created");
    }
}