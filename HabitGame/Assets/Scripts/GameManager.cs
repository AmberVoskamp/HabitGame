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
    [SerializeField] private FadeToBlack _fadeToBlack;
    [SerializeField] private CountDown _countDown;
    [SerializeField] private UIManager _uiManager;

    [Space]
    [SerializeField] private PhasesData _phases;

    [Space]
    [SerializeField] private Vector2 _timeLeftAfterSpikes;

    [Header("Tutorial Text")]
    [SerializeField] private string _walkTutorialText;

    private ConfigManager _configManager;
    private int _currentPhase;
    private List<PhaseData> _levelPhases;
    private PlayerHealth _playerHealth;
    private bool _isLastBoss;

    private void Start()
    {
        if (ConfigManager.Instance != null)
        {
            _configManager = ConfigManager.Instance;
            _configManager.StartLevelData();
        }

        _levelPhases = GetPhases(out float time);
        Phase currentPhase = Instantiate(_levelPhases[_currentPhase].Phase);
        currentPhase.GameManager = this;

        _playerHealth = currentPhase.Spawnpoint.SpawnPlayer();
        _playerHealth.SetData(time, _countDown);

        if (_configManager != null)
        {
            _configManager.SetTotalTime(time);
        }

        ShowTutorial(_walkTutorialText);
    }

    public void ShowTutorial(string text)
    {
        _uiManager.ShowTutorial(text);
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
        int currentDificulty = _configManager.Config.CurrentSpikeDificulty;
        if (spikeFinishTimeLeft < _timeLeftAfterSpikes.x) //To little time left
        {
            updateDificulty = true;
            currentDificulty--;
        }
        else if (spikeFinishTimeLeft > _timeLeftAfterSpikes.y) //To much time left
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

        if (hasFinished)
        {
            _playerHealth.UpgradeAttack();
        }
    }

    public void EnterBossRoom(float bossHealth)
    {
        if (ConfigManager.Instance == null)
        {
            return;
        }

        _configManager.BossRoom(bossHealth);
    }

    public void EndGame(bool killedBoss = false, float timeLeft = 0f)
    {
        _fadeToBlack.Fade();

        if (!TrySetConfig(out ConfigManager config))
        {
            return;
        }

        config.BossFightEnd(killedBoss, _isLastBoss, timeLeft);
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
        List<PhaseData> phases = new()
        {
            GetRandomFirstPhase(),
            _phases.PhasesTwo,
            GetBossPhase()
        };

        phasesTime = 0f;
        foreach (PhaseData phase in phases)
        {
            phasesTime += phase.PhaseTime;
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
        return _levelPhases[_currentPhase].Phase;
    }

    private PhaseData GetRandomFirstPhase()
    {
        int phaseOneCount = _phases.PhasesOne.Length;
        int randomPhaseOneIndex = UnityEngine.Random.Range(0, phaseOneCount);
        return _phases.PhasesOne[randomPhaseOneIndex];
    }

    private PhaseData GetBossPhase()
    {
        int currentBossIndex = 0;
        if (_configManager != null)
        {
            currentBossIndex = _configManager.CurrentBoss();
        }

        int phaseThreeCount = _phases.PhasesThree.Length;
        _isLastBoss = currentBossIndex >= phaseThreeCount - 1;
        if (currentBossIndex >= phaseThreeCount)
        {
            currentBossIndex = phaseThreeCount - 1;
            Debug.LogWarning($"Boss index is higher than should be possible");
        }

        return _phases.PhasesThree[currentBossIndex];
    }

    public void ExitPhase(Phases phases)
    {
        if (_configManager == null)
        {
            return;
        }

        float time = _playerHealth.GetCurrentHealth;
        _configManager.AddPhaseTime(phases, time);
    }
}
