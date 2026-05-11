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
        int totalRounds = 6;

        if (ConfigManager.Instance != null)
        {
            round = ConfigManager.Instance.Config.TotalLevelsPlayed + 1;
            bossKills = ConfigManager.Instance.Config.TotalBossesKilled;
            totalRounds = ConfigManager.Instance.Config.TrainingLevelCount +
                          ConfigManager.Instance.Config.TestLevelCount;
        }

        if (_roundText != null)
            _roundText.text = $"Round: {round}/{totalRounds}";

        if (_bossKillText != null)
            _bossKillText.text = $"Boss Kills: {bossKills}";
    }
}