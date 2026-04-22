using TMPro;
using UnityEngine;

/// <summary>
/// This minigame popup is for showing or not showing the popup
/// </summary>

public class MinigamePopup : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TMP_Text _turotial;
    private bool _minigameDone;

    [field: SerializeField]
    public Minigame Minigame;

    private void Start()
    {
        ShowPopup(false);
    }

    public void ShowTutorial(bool show)
    {
        _turotial.gameObject.SetActive(show);
    }

    public void ShowPopup(bool show, bool minigameSucces = false)
    {
        if (_minigameDone)
        {
            return;
        }

        gameObject.SetActive(show);

        if (!show && !minigameSucces)
        {
            return;
        }

        _minigameDone = minigameSucces;
        _gameManager.MiniGameData(true, minigameSucces);
        StartMiniGame();
    }

    private void StartMiniGame()
    {
        Minigame.StartGame();
    }
}
