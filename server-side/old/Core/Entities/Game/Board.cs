using Core.Entities.Game.Cards;
using Core.Enums.Game;

namespace Core.Entities.Game;

public class Board
{
    public readonly List<Card> Closers = [];
    public readonly List<Card> Rangers = [];
    public readonly List<Card> Sieges = [];
    
    public Board()
    {
        
    }

    public void AddToLine(Card card, FieldLine fieldLine)
    {
        switch (fieldLine)
        {
            case FieldLine.Closer:
            case FieldLine.Ranger:
            case FieldLine.Siege:
            default: 
                throw new NotImplementedException("No line for card with ID: " + card.Id);
        }
    }
}
