using DTT.Utils.Extensions;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// The config manager is for setting the config data while in game
/// </summary>

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance;
    public Config config;

    public int SpikeDificulty
    {
        get { return config.currentSpikeDificulty; }
    }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        config = Config.Load();
    }

    public void UpdateSpikeDificulty(int newDificulty)
    {
        config.currentSpikeDificulty = newDificulty;
        Config.Save(config);
    }

    public void TutorialDone(bool done = true)
    {
        config.tutorialFinished = done;
        Config.Save(config);
    }

    public void StartLevelData()
    {
        int bossIndex = 0;
        float bossHealth = 0;
        if (!config.levelsData.IsNullOrEmpty())
        {
            LevelData lastLevel = config.levelsData[GetCurrentLevelIndex()];
            bossIndex = lastLevel.currentBoss;
            if (lastLevel.killedTheBoss)
            {
                bossIndex++;
            }
            bossHealth = lastLevel.bossHealthLeft;
        }

        LevelData newLevelData = new LevelData()
        {
            index = config.levelsData.Count,
            phaseTimes = new System.Collections.Generic.List<PhaseTimeData>(),
            spikeDificulty = config.currentSpikeDificulty,
            currentBoss = bossIndex,
            bossHealthLeft = bossHealth,
        };

        config.levelsData.Add(newLevelData);
        Config.Save(config);
    }

    public void SetTotalTime(float levelTime)
    {
        int currentIndex = GetCurrentLevelIndex();
        config.levelsData[currentIndex].levelTime = levelTime;
    }

    public void AddPhaseTime(Phases phase, float timeLeft)
    {
        int currentIndex = GetCurrentLevelIndex();
        float totalLevelTime = config.levelsData[currentIndex].levelTime;

        PhaseTimeData phaseTime = new PhaseTimeData()
        {
            phase = phase,
            exitPhaseTime = totalLevelTime - timeLeft,
        };

        config.levelsData[currentIndex].phaseTimes.Add(phaseTime);
    }

    public void Hit(float damage, DamageType type)
    {
        int currentIndex = GetCurrentLevelIndex();

        switch (type)
        {
            case DamageType.Spike:
                config.levelsData[currentIndex].spikesDamageTaken += damage;
                break;
            case DamageType.Boss:
                config.levelsData[currentIndex].bossDamageTaken += damage;
                break;
            case DamageType.Player:
                config.levelsData[currentIndex].bossDamageDone += damage;
                break;
            case DamageType.Other:
            default:
                break;
        }
    }

    public void SpikeLevelData(float endSpikeTime, int newSpikeDificulty)
    {
        int currentIndex = GetCurrentLevelIndex();
        config.levelsData[currentIndex].endSpikeTime = endSpikeTime;

        config.currentSpikeDificulty = newSpikeDificulty;
        Config.Save(config);
    }

    public void SafeWalkData(WalkData.Data[] walkData)
    {
        int currentIndex = GetCurrentLevelIndex();
        config.levelsData[currentIndex].walkData = walkData;
    }

    public void TimeLeftInRangeOfChest(float timeLeft)
    {
        int currentIndex = GetCurrentLevelIndex();
        config.levelsData[currentIndex].timeLeftWhenInChestRange = timeLeft;
    }

    public void TimeLeftDoorOpens(float timeLeft)
    {
        int currentIndex = GetCurrentLevelIndex();
        config.levelsData[currentIndex].timeLeftWhenDoorOpens = timeLeft;
    }

    public void MinigameData(bool hasOpend, bool hasFinished)
    {
        int currentIndex = GetCurrentLevelIndex();
        config.levelsData[currentIndex].opendMinigame = hasOpend;
        config.levelsData[currentIndex].finishedMinigame = hasFinished;
        Config.Save(config);
    }

    public void BossRoom(float bossHealth)
    {
        int currentIndex = GetCurrentLevelIndex();
        config.levelsData[currentIndex].enteredBossRoom = true;
        if (config.levelsData[currentIndex].bossHealthLeft == 0)
        {
            config.levelsData[currentIndex].bossHealthLeft = bossHealth;
        }
        Config.Save(config);
    }

    public void BossFightEnd(bool killedBoss, bool isLastBoss, float timeLeft)
    {
        int currentIndex = GetCurrentLevelIndex();
        config.finishedAllBosses = killedBoss && isLastBoss;
        config.levelsData[currentIndex].killedTheBoss = killedBoss;
        config.levelsData[currentIndex].timeLeft = timeLeft;

        LevelData levelData = config.levelsData[currentIndex];
        float bossHealthLeft = levelData.bossHealthLeft - levelData.bossDamageDone;
        config.levelsData[currentIndex].bossHealthLeft = math.max(0, bossHealthLeft);
        Config.Save(config);
    }

    public int CurrentBoss()
    {
        int currentIndex = GetCurrentLevelIndex();
        if (currentIndex < 0)
        {
            return 0;
        }

        return config.levelsData[currentIndex].currentBoss;
    }

    public float GetBossHealth()
    {
        int currentIndex = GetCurrentLevelIndex();
        if (currentIndex < 0)
        {
            return 0;
        }
        LevelData levelData = config.levelsData[currentIndex];
        return levelData.bossHealthLeft;
    }

    private int GetCurrentLevelIndex()
    {
        return config.levelsData.Count - 1;
    }
}
