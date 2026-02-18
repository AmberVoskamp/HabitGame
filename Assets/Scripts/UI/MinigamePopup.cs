using TMPro;
using UnityEngine;

public class MinigamePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text m_turotial;
    [SerializeField] private Minigame m_minigame;

    private bool _minigameDone;

    private void Start()
    {
        ShowPopup(false);
    }

    public void ShowTutorial(bool show)
    {
        m_turotial.gameObject.SetActive(show);
    }

    public void ShowPopup(bool show, bool minigameSucces = false)
    {
        if (_minigameDone)
        {
            return;
        }

        _minigameDone = minigameSucces;
        gameObject.SetActive(show);
        StartMiniGame();
    }

    private void StartMiniGame()
    {
        m_minigame.StartGame();
    }
}
