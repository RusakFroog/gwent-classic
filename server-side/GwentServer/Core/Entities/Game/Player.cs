using Core.Enums.Game;
using Core.Entities.Game.Cards;

namespace Core.Entities.Game;

public class Player
{
    public readonly string UserId;
    public readonly List<Card> HandCards = [];
    public readonly List<Card> CardsInDeck = [];
    public readonly List<Card> ReleasedCards = [];
    public readonly Fraction Fraction;

    public Player(string userId, Fraction fraction, List<Card> cardsInDeck)
    {
        UserId = userId;
        Fraction = fraction;
        CardsInDeck = cardsInDeck;
    }

    public void DistributeCards(int count)
    {
        Random random = new Random();

        for (int i = 0; count > i; i++)
        {
            int randomIndex = random.Next(0, CardsInDeck.Count);

            Card card = CardsInDeck[randomIndex];

            HandCards.Add(card);
        }
    }
}
