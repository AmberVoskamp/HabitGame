using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FadeToBlack m_fadeToBlack;
    [SerializeField] private PlayerHealth m_playerHealth;

    [Space]
    [SerializeField] private Vector2 m_timeLeftAfterSpikes;

    private ConfigManager _configManager;

    private void Start()
    {
        if (ConfigManager.Instance != null)
        {
            _configManager = ConfigManager.Instance;
            //Set start data
            _configManager.StartLevelData(m_playerHealth.Health);
        }
    }

    public void SpikeSectionDone(float spikeFinishTimeLeft, int maxDificulty)
    {
        if (ConfigManager.Instance == null)
        {
            return;
        }

        #region Get new spike dificulty
        //Todo we are at the end of the spikes check if we have around enough time 
        //If we have to much time we can up the dificulty
        //If we have to litle time we should make it easier 
        bool updateDificulty = false;
        int currentDificulty = _configManager.config.currentSpikeDificulty;
        if (spikeFinishTimeLeft < m_timeLeftAfterSpikes.x) //To little time left
        {
            updateDificulty = true;
            currentDificulty--;
        }
        else if (spikeFinishTimeLeft > m_timeLeftAfterSpikes.y) //To much time left
        {
            updateDificulty = true;
            currentDificulty++;
        }

        if (updateDificulty)
        {
            currentDificulty = math.clamp(currentDificulty, 0, maxDificulty);
            if (ConfigManager.Instance != null)
            {
                ConfigManager.Instance.UpdateSpikeDificulty(currentDificulty);
            }
        }
        #endregion

        _configManager.SpikeLevelData(spikeFinishTimeLeft, currentDificulty);
    }

    public void MiniGameData(bool hasOpend, bool hasFinished)
    {
        if (ConfigManager.Instance == null) 
        {
            return; 
        }

        _configManager.MinigameData(hasOpend, hasFinished);
    }

    public void EnterBossRoom()
    {
        if (ConfigManager.Instance == null)
        {
            return;
        }

        _configManager.BossRoom();
    }

    public void EndGame(bool killedBoss = false, float timeLeft = 0f)
    {
        m_fadeToBlack.Fade();

        if (ConfigManager.Instance == null)
        {
            return;
        }

        _configManager.BossFight(killedBoss, timeLeft);
    }
}
