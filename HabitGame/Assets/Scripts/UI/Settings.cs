using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This settings script is to showcase some of the settings and let players change them
/// </summary>

public class Settings : MonoBehaviour
{
    [SerializeField] private Toggle m_tutorialToggle;
    [SerializeField] private Button m_closeGame;

    private ConfigManager m_configManager;

    private void Start()
    {
        if (ConfigManager.Instance == null)
        {
            gameObject.SetActive(false);
            return;
        }

        m_configManager = ConfigManager.Instance;

        m_tutorialToggle.isOn = m_configManager.config.tutorialFinished;
        m_tutorialToggle.onValueChanged.AddListener(UpdateTutorialCheck);

        m_closeGame.onClick.AddListener(CloseGame);

        gameObject.SetActive(false);
    }

    public void ShowSettings(bool show)
    {
        if (m_configManager == null)
        {
            return;
        }

        gameObject.SetActive(show);
    }

    private void UpdateTutorialCheck(bool toggle)
    {
        m_configManager.TutorialDone(toggle);
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
