using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This settings script is to showcase some of the settings and let players change them
/// </summary>

public class Settings : MonoBehaviour
{
    [SerializeField] private Toggle _tutorialToggle;
    [SerializeField] private Button _closeGame;

    private ConfigManager _configManager;

    private void Start()
    {
        if (ConfigManager.Instance == null)
        {
            gameObject.SetActive(false);
            return;
        }

        _configManager = ConfigManager.Instance;

        _tutorialToggle.isOn = _configManager.Config.TutorialFinished;
        _tutorialToggle.onValueChanged.AddListener(UpdateTutorialCheck);

        _closeGame.onClick.AddListener(CloseGame);

        gameObject.SetActive(false);
    }

    public void ShowSettings(bool show)
    {
        if (_configManager == null)
        {
            return;
        }

        gameObject.SetActive(show);
    }

    private void UpdateTutorialCheck(bool toggle)
    {
        _configManager.TutorialDone(toggle);
    }

    private void CloseGame()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
