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
    [SerializeField] private string _phase2ATutorialText;
    [SerializeField] private string _phase2BTutorialText;
    [SerializeField] private string _phase2ATestTutorialText;
    [SerializeField] private string _phase2BTestTutorialText;

    [SerializeField] private QuestionnaireUI _questionnaireUI;


    private ConfigManager _configManager;
    private int _currentPhase;
    private List<PhaseData> _levelPhases;
    private PlayerHealth _playerHealth;
    private Phase _phase;
    private bool _isLastBoss;
    private bool _isGameEnding = false;

    public Phase CurrentPhase
    {
        set { _phase = value; }
    }

    private void Start()
    {
        if (ConfigManager.Instance != null)
        {
            _configManager = ConfigManager.Instance;
            _configManager.Config.MaxBossIndex = _phases.PhasesThree.Length - 1;
            _configManager.StartLevelData();
        }

        FindFirstObjectByType<RoundCounterUI>()?.UpdateUI();

        _levelPhases = GetPhases(out float time);
        Phase currentPhase = Instantiate(_levelPhases[_currentPhase].Phase);
        currentPhase.GameManager = this;
        currentPhase.OnPhaseStarted();

        _playerHealth = currentPhase.Spawnpoint.SpawnPlayer();
        _playerHealth.SetData(time, _countDown);

        if (_configManager != null)
        {
            _configManager.SetTotalTime(time);
        }

        ShowLongTutorialIfNeeded(); //first training and first testing round
        ShowTutorial(_walkTutorialText);
    }

    public void ShowTutorial(string text)
    {
        _uiManager.ShowTutorial(text);
        Debug.Log(Screen.currentResolution);
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
        // If we already started ending the game, ignore any duplicate calls
        if (_isGameEnding) return; 
        _isGameEnding = true;

        _phase.EndPhase();

        if (!TrySetConfig(out ConfigManager config))
        {
            return;
        }

        config.BossFightEnd(killedBoss, timeLeft);

        // Check if questionnaire should show (just finished round 3 = TrainingLevelCount)
        if (config.Config.CurrentRunLevelCount == config.Config.TrainingLevelCount && !config.Config.QuestionnaireShownThisRun)
        {
            _fadeToBlack.FadeWithCallback(() =>
            {
                _questionnaireUI.Show(() =>
                {
                    // Mark as completed for this run before returning to HomeScene
                    config.Config.QuestionnaireShownThisRun = true;
                    Config.Save(config.Config);
                    
                    SceneSwitchManager.Instance.SwitchScene(Scenes.HomeScene);
                });
            });
        }
        else
        {
            _fadeToBlack.Fade();
        }
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
        bool isTestLevel = _configManager != null && _configManager.Config.IsInTestPhase;
        GetShuffledPhaseTwo(out PhaseData phase2First, out PhaseData phase2Second, isTestLevel);

        List<PhaseData> phases = new()
        {
            GetRandomFirstPhase(),
            phase2First,
            GetRandomFourthPhase(),
            phase2Second,
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

    private PhaseData GetRandomFourthPhase()
    {
        int phaseFourCount = _phases.PhasesFour.Length;
        int randomPhaseFourIndex = UnityEngine.Random.Range(0, phaseFourCount);
        return _phases.PhasesFour[randomPhaseFourIndex];
    }

    private void GetShuffledPhaseTwo(out PhaseData first, out PhaseData second, bool isTestLevel)
    {
        PhaseData[] pool = isTestLevel ? _phases.PhasesTwoTest : _phases.PhasesTwo;
        bool aFirst = UnityEngine.Random.value > 0.5f;
        first  = aFirst ? pool[0] : pool[1];
        second = aFirst ? pool[1] : pool[0];
    }

    private PhaseData GetBossPhase()
    {
        int currentBossIndex = 0;
        if (_configManager != null)
        {
            currentBossIndex = _configManager.CurrentBoss();
        }

        int phaseThreeCount = _phases.PhasesThree.Length;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Config.Save(new Config());
            Debug.Log("Save reset!");
        }
    }

    public void ShowPhaseTutorial(Phases phase)
    {
        Config config = ConfigManager.Instance?.Config;

        bool isFirstTraining = config == null || (!config.IsInTestPhase && config.TotalLevelsPlayed == 0);
        bool isFirstTest = config != null && config.IsInTestPhase &&
                            config.TotalLevelsPlayed == config.TrainingLevelCount;

        if (!isFirstTraining && !isFirstTest) return;

        switch (phase)
        {
            case Phases.Phase2A:
                ShowTutorial(isFirstTest ? _phase2ATestTutorialText : _phase2ATutorialText);
                break;
            case Phases.Phase2B:
                ShowTutorial(isFirstTest ? _phase2BTestTutorialText : _phase2BTutorialText);
                break;
        }
    }

    private void ShowLongTutorialIfNeeded()
    {
        Config config = ConfigManager.Instance?.Config;
        bool isFirstTraining = config == null || (!config.IsInTestPhase && config.TotalLevelsPlayed == 0);
        bool isFirstTest = config != null && config.IsInTestPhase && 
                            config.TotalLevelsPlayed == config.TrainingLevelCount;

        if (isFirstTraining)
            _uiManager.ShowLongTutorial(false);
        else if (isFirstTest)
            _uiManager.ShowLongTutorial(true);
    }
}
