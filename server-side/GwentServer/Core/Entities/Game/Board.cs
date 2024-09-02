using Core.Entities.Game.Cards;

namespace Core.Entities.Game;

public class Board
{
    public readonly List<Card> Closers = [];
    public readonly List<Card> Rangers = [];
    public readonly List<Card> Sieges = [];
    
    public Board()
    {
        
    }
}
