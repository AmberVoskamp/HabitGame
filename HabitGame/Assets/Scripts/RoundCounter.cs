using TMPro;
using UnityEngine;

public class RoundCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _roundText;
    [SerializeField] private TMP_Text _bossKillText;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        int round = 1;
        int bossKills = 0;
        bool isTestPhase = false;
        int totalRounds = 3;
        int trainingLevelCount = 3;

        if (ConfigManager.Instance != null)
        {
            round = ConfigManager.Instance.Config.TotalLevelsPlayed + 1;
            bossKills = ConfigManager.Instance.Config.TotalBossesKilled;
            isTestPhase = ConfigManager.Instance.Config.IsInTestPhase;
            trainingLevelCount = ConfigManager.Instance.Config.TrainingLevelCount;
            totalRounds = isTestPhase ?
                ConfigManager.Instance.Config.TestLevelCount :
                trainingLevelCount;
        }

        int displayRound = isTestPhase ? round - trainingLevelCount : round;
        string phaseLabel = isTestPhase ? "Test" : "Training";

        if (_roundText != null)
            _roundText.text = $"Round: {displayRound}/{totalRounds}";

        if (_bossKillText != null)
            _bossKillText.text = $"Boss Kills: {bossKills}";
    }
}