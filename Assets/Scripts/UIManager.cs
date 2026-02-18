using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CountDown m_countdown;

    [Header("End game UI")]
    [SerializeField] private Image m_endGamePanel;
    [SerializeField] private Button m_homeButton;
    [SerializeField] private TMP_Text m_endText;
    [SerializeField] private string m_winningEndText;
    [SerializeField] private string m_losingEndText;

    private PlayerHealth m_playerHealth;

    private void Start()
    {
        m_playerHealth = PlayerHealth.Instance;
        m_playerHealth.gameObject.SetActive(true);
        m_playerHealth.StartGame();
    }

    public void EndGame(bool hasWon)
    {
        //Todo safe run data
        m_countdown.StopTimer();
        m_homeButton.onClick.AddListener(HomeScreen);
        m_endGamePanel.gameObject.SetActive(true);
        m_endText.text = hasWon ? m_winningEndText : m_losingEndText;

        m_playerHealth.gameObject.SetActive(false);

        
    }

    private void HomeScreen()
    {
        SceneSwitchManager.Instance.SwitchScene(Scenes.HomeScene);
    }
}
