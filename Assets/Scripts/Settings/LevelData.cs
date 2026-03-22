using System;

/// <summary>
/// This is the level data we collect while playing a level
/// </summary>

[Serializable]
public class LevelData
{
    public int index;
    public float levelTime;

    #region spikes
    public int spikeDificulty;
    public float endSpikeTime;
    #endregion

    #region Minigame
    public bool opendMinigame;
    public bool finishedMinigame;
    #endregion

    #region Boss
    public int currentBoss;
    public bool enteredBossRoom;
    public bool killedTheBoss;
    public float timeLeft;
    #endregion


}
