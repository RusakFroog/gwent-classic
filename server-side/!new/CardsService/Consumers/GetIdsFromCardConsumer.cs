using CardsService.Services;
using MassTransit;
using Shared.DTOs.Cards;

namespace CardsService.Consumers;

public class GetIdsFromCardConsumer : IConsumer<GetIdsFromCardRequest>
{
    private readonly ILogger<GetIdsFromCardConsumer> _logger;
    private readonly ICardsService _cardsService;

    public GetIdsFromCardConsumer(ILogger<GetIdsFromCardConsumer> logger, ICardsService cardsService)
    {
        _logger = logger;
        _cardsService = cardsService;
    }

    public async Task Consume(ConsumeContext<GetIdsFromCardRequest> context)
    {
        _logger.LogInformation($"{context.Message.Cards.ToString()}");

        var response = _cardsService.GetIdFromCards(context.Message.Cards);

        await context.RespondAsync(response);
    }
}