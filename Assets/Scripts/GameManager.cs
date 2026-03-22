using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// GameManager is the script where the level data gets saved
/// </summary>

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FadeToBlack m_fadeToBlack;
    [SerializeField] private CountDown m_countDown;
    [SerializeField] private UIManager m_uiManager;

    [Space]
    [SerializeField] private PhasesData m_phases;

    [Space]
    [SerializeField] private Vector2 m_timeLeftAfterSpikes;

    private ConfigManager _configManager;
    private Phases _currentPhase;

    private void Start()
    {
        _currentPhase = Phases.Phase1;
        Phase currentPhase = Instantiate(GetRandomFirstPhase());
        currentPhase.GameManager = this;

        PlayerHealth playerHealth = currentPhase.Spawnpoint.SpawnPlayer();
        playerHealth.SetCountDown(m_countDown);

        if (ConfigManager.Instance != null)
        {
            _configManager = ConfigManager.Instance;
            //Set start data
            _configManager.StartLevelData(playerHealth.Health);
        }

        m_uiManager.ShowTutorial();
    }

    public void SpikeSectionDone(float spikeFinishTimeLeft, int maxDificulty)
    {
        if (!TrySetConfig(out ConfigManager config))
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

        config.SpikeLevelData(spikeFinishTimeLeft, currentDificulty);
    }

    public void MiniGameData(bool hasOpend, bool hasFinished)
    {
        if (!TrySetConfig(out ConfigManager config))
        {
            return;
        }

        config.MinigameData(hasOpend, hasFinished);
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

        if (!TrySetConfig(out ConfigManager config))
        {
            return;
        }

        config.BossFight(killedBoss, timeLeft);
    }

    private bool TrySetConfig(out ConfigManager config)
    {
        if (_configManager == null)
        {
            _configManager = ConfigManager.Instance;
            if (_configManager == null)
            {
                config = null;
                return false;
            }
        }

        config = _configManager;
        return true;
    }

    public Phase NextPhase()
    {
        Debug.Log($"NEXT PHASE");
        if (_currentPhase == Phases.Phase1)
        {
            _currentPhase = Phases.Phase2;
            return m_phases.phasesTwo;
        }
        if (_currentPhase == Phases.Phase2)
        {
            _currentPhase = Phases.Phase3;
            int currentBossIndex = 0;
            if (_configManager != null)
            {
                currentBossIndex = _configManager.CurrentBoss();
            }

            int phaseThreeCount = m_phases.phasesThree.Length;
            if (currentBossIndex >= phaseThreeCount)
            {
                currentBossIndex = phaseThreeCount - 1;
                Debug.LogWarning($"Boss index is higher than should be possible");
            }

            return m_phases.phasesThree[currentBossIndex];
        }

        return null;
    }

    private Phase GetRandomFirstPhase()
    {
        int phaseOneCount = m_phases.phasesOne.Length;
        int randomPhaseOneIndex = UnityEngine.Random.Range(0, phaseOneCount);
        return m_phases.phasesOne[randomPhaseOneIndex];
    }
}
