using System;
using System.Collections.Generic;

/// <summary>
/// This is the level data we collect while playing a level
/// </summary>

[Serializable]
public class LevelData
{
    public int Index;
    public float LevelTime;
    public bool IsTestLevel;

    public List<PhaseTimeData> PhaseTimes;

    #region Phase1
    public int SpikeDificulty;
    public float EndSpikeTime;
    public float SpikesDamageTaken;
    #endregion

    //#region Phase2
    //public float TimeLeftWhenDoorOpens;
    //public float TimeLeftWhenInChestRange;
    //public bool OpendMinigame;
    //public bool FinishedMinigame;
    //public WalkData.Data[] WalkData;
    //public float PassageDamageTaken;
    //#endregion

    #region Phase2 - Minigame
    //public float TimeLeftWhenDoorOpens;
    //public float TimeLeftWhenInChestRange;
    public bool OpendMinigame;
    public bool FinishedMinigame;
    public WalkData.Data[] WalkData;
    #endregion

    #region Phase2 - Passage
    public float PassageDamageTaken;
    public WalkData.Data[] WalkDataPassage;
    #endregion

    #region Phase3
    public int CurrentBoss;
    public bool EnteredBossRoom;
    public bool KilledTheBoss;
    public float BossDamageTaken;
    public float BossDamageDone;
    public float BossHealthLeft;
    public float TimeLeft;
    #endregion
}

[Serializable]
public struct PhaseTimeData
{
    public Phases Phase;
    public float ExitPhaseTime;
}
