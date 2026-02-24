using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;

    [SerializeField] private TMP_Text m_countdownText;
    [SerializeField] private TMP_Text m_timeLostPrefab;
    [SerializeField] private float m_lostTimeToTimer = 1f;

    //should remove
    public float CurrentTime
    {
        get { return 10; }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateTimer(float time)
    {
        #region Check if there is still time left
        if (time <= 0f)
        {
            m_countdownText.text = "00:00";
            m_gameManager.EndGame();
            return;
        }
        #endregion

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        m_countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void LoseTime(float lostTime, Vector3 screenPosition, Action completeAction)
    {
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
                completeAction?.Invoke();
                /*_time -= lostTime;
                UpdateTimer();*/
            });
        #endregion
    }
}
