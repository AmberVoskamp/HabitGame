using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TMP_Text m_countdownText;
    [SerializeField] private float m_secondCountdown;

    private float m_counter;
    private bool m_isPlaying;

    private void Start()
    {
        m_counter = m_secondCountdown;
        SetText();
        gameObject.SetActive(false);
    }

    public void StartCountdown()
    {
        m_isPlaying = true;
    }

    private void SetText()
    {
        int minutes = Mathf.FloorToInt(m_counter / 60);
        int seconds = Mathf.FloorToInt(m_counter % 60);
        m_countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //Set the countdown text 
    }

    private void Update()
    {
        if (!m_isPlaying || m_counter <= 0)
        {
            return;
        }

        m_counter -= Time.deltaTime;
        SetText();
    }
}
