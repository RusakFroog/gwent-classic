namespace Core.Models.Game.Players;

public record PlayerReadiness()
{
    public bool FirstPlayer { get; set; } 
    public bool SecondPlayer { get; set; }
    
    public PlayerReadiness(bool firstPlayer, bool secondPlayer) : this()
    {
        FirstPlayer = firstPlayer;
        SecondPlayer = secondPlayer;
    }
}