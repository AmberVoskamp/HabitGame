using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

/// <summary>
/// This countdown visualizes the time the player has left
/// </summary>

public class CountDown : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private TMP_Text _countdownText;
    [SerializeField] private TMP_Text _timeLostPrefab;
    [SerializeField] private float _lostTimeToTimer = 1f;

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
            _countdownText.text = "00:00";
            _gameManager.EndGame();
            return;
        }
        #endregion

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        _countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //Spawns a text object that shows how much time you lost when you got hit
    public void LoseTime(float lostTime, Vector3 screenPosition, Action completeAction)
    {
        //Spawn the time you lose at the player position on the canvas
        TMP_Text timeLost = Instantiate(_timeLostPrefab, transform);
        timeLost.text = $"-{lostTime:F0}s";
        timeLost.transform.position = screenPosition;

        #region 
        timeLost.transform.DOMove(transform.position, _lostTimeToTimer)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(timeLost);
                completeAction?.Invoke();
            });
        #endregion
    }
}
