using IdentityService.Services;
using MassTransit;
using Shared.DTOs.Identity;

namespace IdentityService.Consumers;

public class LoginAccountConsumer : IConsumer<LoginAccountRequest>
{
    private readonly ILogger<LoginAccountConsumer> _logger;
    private readonly IAccountService _accountService;

    public LoginAccountConsumer(ILogger<LoginAccountConsumer> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    public async Task Consume(ConsumeContext<LoginAccountRequest> context)
    {
        _logger.LogInformation($"l={context.Message.Login} p={context.Message.Password}");

        var response = await _accountService.Login(context.Message.Login, context.Message.Password);

        await context.RespondAsync(response);
    }
}
