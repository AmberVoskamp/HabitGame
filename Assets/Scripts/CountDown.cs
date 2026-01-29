using DG.Tweening;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private UIManager m_uiManager;

    [SerializeField] private TMP_Text m_countdownText;
    [SerializeField] private TMP_Text m_timeLostPrefab;
    [SerializeField] private float m_lostTimeToTimer = 1f;

    private float _time;
    private bool _isPlaying;

    public float CurrentTime
    {
        get { return _time; }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartCountdown(float timer)
    {
        _time = timer;
        UpdateTimer();
        _isPlaying = true;
    }

    public void LoseTime(float lostTime, Vector3 screenPosition)
    {
        if (!_isPlaying)
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
                _time -= lostTime;
                UpdateTimer();
            });
        #endregion
    }

    public void StopTimer()
    {
        _isPlaying = false;
    }

    private void UpdateTimer()
    {
        #region Check if there is still time left
        if (_time <= 0f)
        {
            m_uiManager.EndGame(false);
            m_countdownText.text = "00:00";
            return;
        }
        #endregion

        int minutes = Mathf.FloorToInt(_time / 60);
        int seconds = Mathf.FloorToInt(_time % 60);
        m_countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Update()
    {
        if (!_isPlaying || _time <= 0)
        {
            return;
        }

        _time -= Time.deltaTime;
        UpdateTimer();
    }
}
