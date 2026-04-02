using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The home screen manager handels the home screen UI inputs
/// </summary>

public class HomeScreenManager : MonoBehaviour
{
    [SerializeField] private Button m_playButton;
    [SerializeField] private Button m_downloadButton;
    [SerializeField] private Image m_donwPlaying;

    private ConfigManager _configManager;

    private void Start()
    {
        m_playButton.onClick.AddListener(GameScene);
        DonePanel(false);

        if (_configManager == null)
        {
            _configManager = ConfigManager.Instance;
            m_downloadButton.onClick.AddListener(DownloadButton);

            if (_configManager != null)
            {
                DonePanel(_configManager.config.finishedAllBosses);
            }
        }
    }

    private void DonePanel(bool enabled)
    {
        m_donwPlaying.gameObject.SetActive(enabled);
        m_playButton.gameObject.SetActive(!enabled);
    }

    private void DownloadButton()
    {
        if (_configManager == null)
        {
            return;
        }

        Config.Download(_configManager.config);
    }

    private void GameScene()
    {
        SceneSwitchManager.Instance.SwitchScene(Scenes.GameScene);
    }
}
