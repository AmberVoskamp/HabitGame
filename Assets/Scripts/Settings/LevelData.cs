using System;
using System.Collections.Generic;

/// <summary>
/// This is the level data we collect while playing a level
/// </summary>

[Serializable]
public class LevelData
{
    public int index;
    public float levelTime;

    public List<PhaseTimeData> phaseTimes;

    #region Phase1
    public int spikeDificulty;
    public float endSpikeTime;
    public float spikesDamageTaken;
    #endregion

    #region Phase2
    public float timeLeftWhenDoorOpens;
    public float timeLeftWhenInChestRange;
    public bool opendMinigame;
    public bool finishedMinigame;
    public WalkData.Data[] walkData;
    #endregion

    #region Phase3
    public int currentBoss;
    public bool enteredBossRoom;
    public bool killedTheBoss;
    public float bossDamageTaken;
    public float bossDamageDone; 
    public float timeLeft;
    #endregion
}

[Serializable]
public struct PhaseTimeData
{
    public Phases phase;
    public float exitPhaseTime;
}
