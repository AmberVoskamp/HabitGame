using TMPro;
using UnityEngine;

/// <summary>
/// This minigame popup is for showing or not showing the popup
/// </summary>

public class MinigamePopup : MonoBehaviour
{
    public static MinigamePopup Instance;

    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private TMP_Text m_turotial;
    [SerializeField] private Minigame m_minigame;

    private bool _minigameDone;

    public Minigame Minigame
    {
        get { return m_minigame; } 
    }

    private void Start()
    {
        Instance = this;
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

        m_gameManager.MiniGameData(true, minigameSucces);

        _minigameDone = minigameSucces;
        gameObject.SetActive(show);
        StartMiniGame();
    }

    private void StartMiniGame()
    {
        m_minigame.StartGame();
    }
}
