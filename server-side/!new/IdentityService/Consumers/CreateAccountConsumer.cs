using IdentityService.Services;
using MassTransit;
using Shared.DTOs.Identity;

namespace IdentityService.Consumers;

public class CreateAccountConsumer : IConsumer<CreateAccountRequest>
{
    private readonly ILogger<CreateAccountConsumer> _logger;
    private readonly IBusControl _busControl;
    private readonly IAccountService _accountService;
    
    private readonly Uri _cardsQueue = new Uri("rabbitmq://localhost/CardsQueue");

    public CreateAccountConsumer(ILogger<CreateAccountConsumer> logger, IBusControl busControl, IAccountService accountService)
    {
        _logger = logger;
        _busControl = busControl;
        _accountService = accountService;
    }

    public async Task Consume(ConsumeContext<CreateAccountRequest> context)
    {
        _logger.LogInformation($"e={context.Message.Email} l={context.Message.Login} p={context.Message.Password}");

        var response = await _accountService.Create(context.Message.Login, context.Message.Login, context.Message.Email, context.Message.Password);

        await context.RespondAsync(response);
    }
}