using DTT.Utils.Extensions;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// The config manager is for setting the config data while in game
/// </summary>

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance;
    public Config Config;

    public int SpikeDificulty => Config.CurrentSpikeDificulty;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Config = Config.Load();
    }

    public void UpdateSpikeDificulty(int newDificulty)
    {
        Config.CurrentSpikeDificulty = newDificulty;
        Config.Save(Config);
    }

    public void TutorialDone(bool done = true)
    {
        Config.TutorialFinished = done;
        Config.Save(Config);
    }

    public void StartLevelData()
    {
        int bossIndex = 0;
        float bossHealth = 0;
        if (!Config.LevelsData.IsNullOrEmpty())
        {
            LevelData lastLevel = Config.LevelsData[GetCurrentLevelIndex()];
            bossIndex = lastLevel.CurrentBoss;
            if (lastLevel.KilledTheBoss)
            {
                bossIndex++;
                if (!Config.TutorialFinished)
                {
                    TutorialDone(true);
                }
            }
            bossHealth = lastLevel.BossHealthLeft;
        }

        LevelData newLevelData = new()
        {
            Index = Config.LevelsData.Count,
            PhaseTimes = new System.Collections.Generic.List<PhaseTimeData>(),
            SpikeDificulty = Config.CurrentSpikeDificulty,
            CurrentBoss = bossIndex,
            BossHealthLeft = bossHealth,
        };

        Config.LevelsData.Add(newLevelData);
        Config.Save(Config);
    }

    public void SetTotalTime(float levelTime)
    {
        int currentIndex = GetCurrentLevelIndex();
        Config.LevelsData[currentIndex].LevelTime = levelTime;
    }

    public void AddPhaseTime(Phases phase, float timeLeft)
    {
        int currentIndex = GetCurrentLevelIndex();
        float totalLevelTime = Config.LevelsData[currentIndex].LevelTime;

        PhaseTimeData phaseTime = new()
        {
            Phase = phase,
            ExitPhaseTime = totalLevelTime - timeLeft,
        };

        Config.LevelsData[currentIndex].PhaseTimes.Add(phaseTime);
    }

    public void Hit(float damage, DamageType type)
    {
        int currentIndex = GetCurrentLevelIndex();

        switch (type)
        {
            case DamageType.Spike:
                Config.LevelsData[currentIndex].SpikesDamageTaken += damage;
                break;
            case DamageType.Boss:
                Config.LevelsData[currentIndex].BossDamageTaken += damage;
                break;
            case DamageType.Player:
                Config.LevelsData[currentIndex].BossDamageDone += damage;
                break;
            case DamageType.Other:
            default:
                break;
        }
    }

    public void SpikeLevelData(float endSpikeTime, int newSpikeDificulty)
    {
        int currentIndex = GetCurrentLevelIndex();
        Config.LevelsData[currentIndex].EndSpikeTime = endSpikeTime;

        Config.CurrentSpikeDificulty = newSpikeDificulty;
        Config.Save(Config);
    }

    public void SafeWalkData(WalkData.Data[] walkData)
    {
        int currentIndex = GetCurrentLevelIndex();
        Config.LevelsData[currentIndex].WalkData = walkData;
    }

    public void TimeLeftInRangeOfChest(float timeLeft)
    {
        int currentIndex = GetCurrentLevelIndex();
        Config.LevelsData[currentIndex].TimeLeftWhenInChestRange = timeLeft;
    }

    public void TimeLeftDoorOpens(float timeLeft)
    {
        int currentIndex = GetCurrentLevelIndex();
        Config.LevelsData[currentIndex].TimeLeftWhenDoorOpens = timeLeft;
    }

    public void MinigameData(bool hasOpend, bool hasFinished)
    {
        int currentIndex = GetCurrentLevelIndex();
        Config.LevelsData[currentIndex].OpendMinigame = hasOpend;
        Config.LevelsData[currentIndex].FinishedMinigame = hasFinished;
        Config.Save(Config);
    }

    public void BossRoom(float bossHealth)
    {
        int currentIndex = GetCurrentLevelIndex();
        Config.LevelsData[currentIndex].EnteredBossRoom = true;
        if (Config.LevelsData[currentIndex].BossHealthLeft == 0)
        {
            Config.LevelsData[currentIndex].BossHealthLeft = bossHealth;
        }
        Config.Save(Config);
    }

    public void BossFightEnd(bool killedBoss, bool isLastBoss, float timeLeft)
    {
        int currentIndex = GetCurrentLevelIndex();
        Config.FinishedAllBosses = killedBoss && isLastBoss;
        Config.LevelsData[currentIndex].KilledTheBoss = killedBoss;
        Config.LevelsData[currentIndex].TimeLeft = timeLeft;

        LevelData levelData = Config.LevelsData[currentIndex];
        float bossHealthLeft = levelData.BossHealthLeft - levelData.BossDamageDone;
        Config.LevelsData[currentIndex].BossHealthLeft = math.max(0, bossHealthLeft);
        Config.Save(Config);
    }

    public int CurrentBoss()
    {
        int currentIndex = GetCurrentLevelIndex();
        return currentIndex < 0 ? 0 : Config.LevelsData[currentIndex].CurrentBoss;
    }

    public float GetBossHealth()
    {
        int currentIndex = GetCurrentLevelIndex();
        if (currentIndex < 0)
        {
            return 0;
        }
        LevelData levelData = Config.LevelsData[currentIndex];
        return levelData.BossHealthLeft;
    }

    private int GetCurrentLevelIndex()
    {
        return Config.LevelsData.Count - 1;
    }
}
