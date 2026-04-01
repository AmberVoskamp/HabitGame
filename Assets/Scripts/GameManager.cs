using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static PhasesData;

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
    private int _currentPhase;
    private List<PhaseData> _levelPhases;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        if (ConfigManager.Instance != null)
        {
            _configManager = ConfigManager.Instance;
            //Set start data
        }

        _levelPhases = GetPhases(out float time);
        Phase currentPhase = Instantiate(_levelPhases[_currentPhase].phase);
        currentPhase.GameManager = this;

        _playerHealth = currentPhase.Spawnpoint.SpawnPlayer();
        _playerHealth.SetData(time, m_countDown);
        Debug.Log($"Time {time}");
        if (_configManager != null)
        {
            _configManager.StartLevelData(time);
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

    private List<PhaseData> GetPhases(out float phasesTime)
    {
        List<PhaseData> phases = new List<PhaseData>();

        phases.Add(GetRandomFirstPhase());
        phases.Add(m_phases.phasesTwo);
        phases.Add(GetBossPhase());

        phasesTime = 0f;
        foreach (PhaseData phase in phases)
        {
            phasesTime += phase.phaseTime;
        }

        return phases;
    }

    public Phase NextPhase()
    {
        if (_currentPhase >= _levelPhases.Count)
        {
            return null;
        }

        _currentPhase++;
        return _levelPhases[_currentPhase].phase;
    }

    private PhaseData GetRandomFirstPhase()
    {
        int phaseOneCount = m_phases.phasesOne.Length;
        int randomPhaseOneIndex = UnityEngine.Random.Range(0, phaseOneCount);
        return m_phases.phasesOne[randomPhaseOneIndex];
    }

    private PhaseData GetBossPhase()
    {
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

    public void ExitPhase(Phases phases)
    {
        if (_configManager == null)
        {
            return;
        }

        float time = _playerHealth.CurrentHealth;
        _configManager.AddPhaseTime(phases, time);
    }
}
