using Core.Models.Game.Cards;

namespace Core.Models.Game;

public class Board
{
    public readonly List<Card> Closers = [];
    public readonly List<Card> Rangers = [];
    public readonly List<Card> Sieges = [];
    
    public Board()
    {
        
    }
}
