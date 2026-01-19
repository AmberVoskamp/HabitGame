using UnityEngine;
using UnityEngine.UI;

public class MinigamePopup : MonoBehaviour
{
    [SerializeField] private Button m_closeButton;
    [SerializeField] private Button m_playButton;
    [SerializeField] private Minigame m_minigame;

    private bool _minigameDone;

    private void Start()
    {
        ShowPopup(false);
        m_closeButton.onClick.AddListener(() => { ShowPopup(false); });
        m_playButton.onClick.AddListener(StartMiniGame);
    }

    public void ShowPopup(bool show, bool minigameSucces = false)
    {
        if (_minigameDone)
        {
            return;
        }

        _minigameDone = minigameSucces;
        gameObject.SetActive(show);
    }

    private void StartMiniGame()
    {
        m_playButton.gameObject.SetActive(false);
        m_minigame.StartGame();
    }
}
