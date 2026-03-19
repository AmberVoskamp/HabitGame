using TMPro;
using UnityEngine;

/// <summary>
/// This tutorial script is to toggle and showcase the tutorial 
/// </summary>

public class Tutorial : MonoBehaviour
{
    [SerializeField] private MinigamePopup m_minigamePopup;
    [SerializeField] private TMP_Text m_text;
    [SerializeField] private string[] m_tutorialText;
    [SerializeField] private bool m_doTutorial;

    private int _tutorialIndex = 0;
    private ConfigManager _configManager;

    private void Awake()
    {
        gameObject.SetActive(false);

        _configManager = ConfigManager.Instance;
        if (_configManager != null)
        {
            m_doTutorial = !_configManager.config.tutorialFinished;
        }
       
        m_minigamePopup.ShowTutorial(m_doTutorial);
    }

    public void ShowTutorial()
    {
        if (!m_doTutorial || _tutorialIndex >= m_tutorialText.Length)
        {
            return;
        }

        m_text.text = m_tutorialText[_tutorialIndex];
        _tutorialIndex++;
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    //Input UI click
    public void ContinueGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;

        if (_configManager != null && _tutorialIndex >= m_tutorialText.Length)
        {
            _configManager.TutorialDone();
        }
    }
}
