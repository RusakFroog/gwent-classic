using IdentityService.Services;
using MassTransit;
using Shared.DTOs.Identity;

namespace IdentityService.Consumers;

public class UpdateAccountConsumer : IConsumer<UpdateAccountRequest>
{
    private readonly ILogger<UpdateAccountConsumer> _logger;
    private readonly IAccountService _accountService;

    public UpdateAccountConsumer(ILogger<UpdateAccountConsumer> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    public async Task Consume(ConsumeContext<UpdateAccountRequest> context)
    {
        _logger.LogInformation($"e={context.Message.Email} d={context.Message.Decks} n={context.Message.Name} p={context.Message.Password}");

        var response = await _accountService.Update(context.Message.Id, context.Message.Login, context.Message.Name, context.Message.Email, context.Message.Password, context.Message.Decks);

        await context.RespondAsync(response);
    }
}