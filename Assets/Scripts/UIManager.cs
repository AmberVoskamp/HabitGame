using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_startUI;
    [SerializeField] private CountDown m_countdown;

    [Header("End game UI")]
    [SerializeField] private Image m_endGamePanel;
    [SerializeField] private TMP_Text m_endText;
    [SerializeField] private string m_winningEndText;
    [SerializeField] private string m_losingEndText;

    private PlayerHealth m_playerHealth;

    public void StartGame()
    {
        m_startUI.SetActive(false);
        m_playerHealth = PlayerHealth.Instance;
        m_playerHealth.gameObject.SetActive(true);
        m_playerHealth.StartGame();
    }

    public void EndGame(bool hasWon)
    {
        m_countdown.StopTimer();
        m_endGamePanel.gameObject.SetActive(true);
        m_endText.text = hasWon ? m_winningEndText : m_losingEndText;

        m_playerHealth.gameObject.SetActive(false);
    }
}
