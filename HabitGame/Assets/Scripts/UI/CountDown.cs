using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

/// <summary>
/// This countdown visualizes the time the player has left
/// </summary>

public class CountDown : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;

    [SerializeField] private TMP_Text m_countdownText;
    [SerializeField] private TMP_Text m_timeLostPrefab;
    [SerializeField] private float m_lostTimeToTimer = 1f;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    //Updates the time shown in the countdown and will visualize like 1:00 for one minute
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

    //Spawns a text object that shows how much time you lost when you got hit
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
            });
        #endregion
    }
}
