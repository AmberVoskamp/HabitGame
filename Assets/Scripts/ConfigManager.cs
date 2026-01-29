using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance;
    public Config m_config;

    public int SpikeDificulty
    {
        get { return m_config.spikeDificulty; }
    }

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        m_config = Config.Load();
    }

    public void UpdateSpikeDificulty(int newDificulty)
    {
        m_config.spikeDificulty = newDificulty;
        Config.Save(m_config);
    }
}
