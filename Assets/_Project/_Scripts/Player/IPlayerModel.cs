using System;


namespace SpaceshipBattle3D.Player
{
    /// <summary>
    /// Read-only contract for player state.
    /// Follows the Pull-based Observer pattern — events carry no data.
    /// Subscribers must read the relevant property directly after receiving the event.
    /// </summary>
    public interface IPlayerModel
    { 
        float Speed { get; }
        int   Lives { get; }
        int   Score { get; }

        event Action OnLivesChanged;
        event Action OnScoreChanged;
    }
}