using UnityEngine;

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

    public void StartLevelData(float levelTime)
    {
        LevelData newLevelData = new LevelData()
        {
            index = config.levelsData.Count,
            levelTime = levelTime,
            spikeDificulty = config.currentSpikeDificulty,
        };

        config.levelsData.Add(newLevelData);
        Config.Save(config);
    }

    public void SpikeLevelData(float endSpikeTime, int newSpikeDificulty)
    {
        int currentIndex = config.levelsData.Count - 1;
        config.levelsData[currentIndex].endSpikeTime = endSpikeTime;

        config.currentSpikeDificulty = newSpikeDificulty;
        Config.Save(config);
        //spikeHitTimes
    }

        
    public void MinigameData(bool hasOpend, bool hasFinished)
    {
        int currentIndex = config.levelsData.Count - 1;
        config.levelsData[currentIndex].opendMinigame = hasOpend;
        config.levelsData[currentIndex].finishedMinigame = hasFinished;
        Config.Save(config);
    }

    public void BossRoom()
    {
        int currentIndex = config.levelsData.Count - 1;
        config.levelsData[currentIndex].enteredBossRoom = true;
        Config.Save(config);
    }

    public void BossFight(bool killedBoss, float timeLeft)
    {
        int currentIndex = config.levelsData.Count - 1;
        config.levelsData[currentIndex].killedTheBoss = killedBoss;
        config.levelsData[currentIndex].timeLeft = timeLeft;
        Config.Save(config);
    }
}
