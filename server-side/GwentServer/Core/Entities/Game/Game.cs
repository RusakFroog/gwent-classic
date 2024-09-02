namespace Core.Entities.Game;

public class Game
{
    public readonly Player FirstPlayer;
    public readonly Player SecondPlayer;
    public readonly Board Board;

    private const int CARDS_AT_START = 10;

    public Game(Player firstPlayer, Player secondPlayer)
    {
        Board = new Board();
        
        FirstPlayer = firstPlayer;
        SecondPlayer = secondPlayer;
    }

    public void StartGame()
    {
        FirstPlayer.DistributeCards(CARDS_AT_START);
        SecondPlayer.DistributeCards(CARDS_AT_START);
    }
}