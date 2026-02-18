using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance;
    public Config config;

    public int SpikeDificulty
    {
        get { return config.spikeDificulty; }
    }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        config = Config.Load();
    }

    public void UpdateSpikeDificulty(int newDificulty)
    {
        config.spikeDificulty = newDificulty;
        Config.Save(config);
    }

    public void TutorialDone(bool done = true)
    {
        config.tutorialFinished = done;
        Config.Save(config);
    }
}
