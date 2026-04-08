using TMPro;
using UnityEngine;

/// <summary>
/// This tutorial script is to toggle and showcase the tutorial 
/// </summary>

public class Tutorial : MonoBehaviour
{
    [SerializeField] private MinigamePopup m_minigamePopup;
    [SerializeField] private TMP_Text m_text;
    [SerializeField] private bool m_doTutorial;

    private bool _tutorialFinished;
    private ConfigManager _configManager;

    private void Awake()
    {
        _configManager = ConfigManager.Instance;
        if (_configManager != null)
        {
            m_doTutorial = !_configManager.config.tutorialFinished;
        }
       
        m_minigamePopup.ShowTutorial(m_doTutorial);
        gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        Debug.Log("Tutorial");
    }

    public void ShowTutorial(string tutorialText)
    {
        if (!m_doTutorial)
        {
            return;
        }
        m_text.text = tutorialText;
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    //Input UI click
    public void ContinueGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
