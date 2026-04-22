using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The home screen manager handels the home screen UI inputs
/// </summary>

public class HomeScreenManager : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _downloadButton;
    [SerializeField] private Image _donwPlaying;

    private ConfigManager _configManager;

    private void Start()
    {
        _playButton.onClick.AddListener(GameScene);
        DonePanel(false);

        if (_configManager == null)
        {
            _configManager = ConfigManager.Instance;
            _downloadButton.onClick.AddListener(DownloadButton);

            if (_configManager != null)
            {
                DonePanel(_configManager.Config.FinishedAllBosses);
            }
        }
    }

    private void DonePanel(bool enabled)
    {
        _donwPlaying.gameObject.SetActive(enabled);
        _playButton.gameObject.SetActive(!enabled);
    }

    private void DownloadButton()
    {
        if (_configManager == null)
        {
            return;
        }

        Config.Download(_configManager.Config);
    }

    private void GameScene()
    {
        SceneSwitchManager.Instance.SwitchScene(Scenes.GameScene);
    }
}
