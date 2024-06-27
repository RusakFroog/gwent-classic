using Core.Enums.Game;
using Core.Models.Game.Cards;

namespace Core.Models.Game.Players;

public class Player
{
    private const int CARDS_AT_START = 10;

    public readonly string UserId;
    public readonly List<Card> HandCards = [];
    public readonly List<Card> PoolCards = [];
    public readonly List<Card> ReleasedCards = [];
    public readonly Fraction Fraction;
    
    public Player(string userId, Fraction fraction, List<Card> cardsPool)
    {
        UserId = userId;
        Fraction = fraction;
        PoolCards = cardsPool;
    }
    
    public void DistributeCards()
    {
        Random random = new Random();

        for (int i = 0; CARDS_AT_START > i; i++)
        {
            int randomIndex = random.Next(0, PoolCards.Count);

            Card card = PoolCards[randomIndex];
            
            PoolCards.RemoveAt(randomIndex);
            HandCards.Add(card);
        }
    }
}
