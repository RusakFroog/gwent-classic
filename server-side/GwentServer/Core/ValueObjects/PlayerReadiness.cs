namespace Core.ValueObjects;

public record PlayerReadiness
{
    public bool FirstPlayer { get; set; }
    public bool SecondPlayer { get; set; }

    public PlayerReadiness(bool firstPlayer, bool secondPlayer)
    {
        FirstPlayer = firstPlayer;
        SecondPlayer = secondPlayer;
    }
}