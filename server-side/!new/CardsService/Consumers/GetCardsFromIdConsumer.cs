using CardsService.Services;
using MassTransit;
using Shared.DTOs.Cards;

namespace CardsService.Consumers;

public class GetCardsFromIdConsumer : IConsumer<GetCardsFromIdRequest>
{
    private readonly ILogger<GetCardsFromIdConsumer> _logger;
    private readonly ICardsService _cardsService;

    public GetCardsFromIdConsumer(ILogger<GetCardsFromIdConsumer> logger, ICardsService cardsService)
    {
        _logger = logger;
        _cardsService = cardsService;
    }

    public async Task Consume(ConsumeContext<GetCardsFromIdRequest> context)
    {
        _logger.LogInformation($"{context.Message.CardsId}");

        var response = await _cardsService.GetCardsFromIds(context.Message.CardsId);

        await context.RespondAsync(response);
    }
}