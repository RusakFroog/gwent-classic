using Shared.DTOs.Cards;
using Shared.Models;

namespace CardsService.Services;

public interface ICardsService
{
    Task<GetCardsFromIdResponse> GetCardsFromIds(List<int> cardsId);
    GetIdsFromCardResponse GetIdFromCards(List<Card> cards);
}
