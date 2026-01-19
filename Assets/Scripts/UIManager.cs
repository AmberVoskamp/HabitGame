using System.Threading;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_startUI;
    [SerializeField] private HealthUI m_healthUI;
    [SerializeField] private CountDown m_countdown;

    public void StartGame()
    {
        m_startUI.SetActive(false);
        PlayerHealth playerHealth = PlayerHealth.Instance;
        playerHealth.gameObject.SetActive(true);
        m_healthUI.gameObject.SetActive(true);
        m_countdown.gameObject.SetActive(true);
        m_countdown.StartCountdown();
    }
}
