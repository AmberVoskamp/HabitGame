using DG.Tweening;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private UIManager m_uiManager;

    [SerializeField] private TMP_Text m_countdownText;
    [SerializeField] private TMP_Text m_timeLostPrefab;
    [SerializeField] private float m_lostTimeToTimer = 1f;

    private float m_counter;
    private bool m_isPlaying;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartCountdown(float timer)
    {
        m_counter = timer;
        UpdateTimer();
        m_isPlaying = true;
    }

    public void LoseTime(float lostTime, Vector3 screenPosition)
    {
        if (!m_isPlaying)
        {
            return;
        }

        //Spawn the time you lose at the player position on the canvas
        TMP_Text timeLost = Instantiate(m_timeLostPrefab, transform);
        timeLost.text = $"-{lostTime.ToString("F0")}s";
        timeLost.transform.position = screenPosition;

        #region 
        timeLost.transform.DOMove(transform.position, m_lostTimeToTimer)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(timeLost);
                m_counter -= lostTime;
                UpdateTimer();
            });
        #endregion
    }

    public void StopTimer()
    {
        m_isPlaying = false;
    }

    private void UpdateTimer()
    {
        #region Check if there is still time left
        if (m_counter <= 0f)
        {
            m_uiManager.EndGame(false);
            m_countdownText.text = "00:00";
            return;
        }
        #endregion

        int minutes = Mathf.FloorToInt(m_counter / 60);
        int seconds = Mathf.FloorToInt(m_counter % 60);
        m_countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Update()
    {
        if (!m_isPlaying || m_counter <= 0)
        {
            return;
        }

        m_counter -= Time.deltaTime;
        UpdateTimer();
    }
}
