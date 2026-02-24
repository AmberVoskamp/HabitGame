using TMPro;
using UnityEngine;
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
}
