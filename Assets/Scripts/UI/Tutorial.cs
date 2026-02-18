using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private MinigamePopup m_minigamePopup;
    [SerializeField] private TMP_Text m_text;
    [SerializeField] private string[] m_tutorialText;
    [SerializeField] private bool m_doTutorial;

    private int m_tutorialIndex = 0;
    private ConfigManager m_configManager;

    private void Start()
    {
        gameObject.SetActive(false);

        m_configManager = ConfigManager.Instance;
        if (m_configManager != null)
        {
            m_doTutorial = !m_configManager.config.tutorialFinished;
        }
        
        if (m_doTutorial)
        {
            ShowTutorial();
        }
        m_minigamePopup.ShowTutorial(m_doTutorial);
    }

    public void ShowTutorial()
    {
        if (!m_doTutorial || m_tutorialIndex >= m_tutorialText.Length)
        {
            return;
        }

        m_text.text = m_tutorialText[m_tutorialIndex];
        m_tutorialIndex++;
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    //Input UI click
    public void ContinueGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;

        if (m_configManager != null && m_tutorialIndex >= m_tutorialText.Length)
        {
            m_configManager.TutorialDone();
        }
    }
}
