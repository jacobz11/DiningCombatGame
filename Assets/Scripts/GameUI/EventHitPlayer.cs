using System;

internal class EventHitPlayer : EventArgs
{
    internal int Kill;
    internal int Score;

    public EventHitPlayer(int kills, int score)
    {
        this.Kill = kills;
        this.Score = score;
    }
}