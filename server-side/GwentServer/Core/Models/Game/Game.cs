using Core.Models.Game.Players;

namespace Core.Models.Game;

public class Game
{
    public readonly Player FirstPlayer;
    public readonly Player SecondPlayer;
    public readonly Board Board;
    
    public Game(Player firstPlayer, Player secondPlayer)
    {
        Board = new Board();
        
        FirstPlayer = firstPlayer;
        SecondPlayer = secondPlayer;
    }

    public void StartGame()
    {
        FirstPlayer.DistributeCards();
        SecondPlayer.DistributeCards();
    }
}