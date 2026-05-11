using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private MinigamePopup _minigamePopup;
    [SerializeField] private Tutorial _tutorial;
    [SerializeField] private LongTutorial _longTutorial;

    private void Awake()
    {
        Instance = this;
        _tutorial.gameObject.SetActive(false);
    }

    public void ShowMinigame(bool show)
    {
        _minigamePopup.ShowPopup(show);
    }

    public void MinigameTap()
    {
        if (!_minigamePopup.isActiveAndEnabled)
        {
            return;
        }
        _minigamePopup.TapInput();
    }

    public void TutorialClick()
    {
        if (LongTutorial.IsShowing)
        {
            LongTutorialNextPage();
            return;
        }
        _tutorial.ContinueGame();
    }

    public void ShowTutorial(string tutorialText)
    {
        // _tutorial.gameObject.SetActive(true);
        _tutorial.ShowTutorial(tutorialText);
    }

    public void ShowLongTutorial(bool isTest)
    {
        if (isTest)
            _longTutorial.ShowTestTutorial();
        else
            _longTutorial.ShowTrainingTutorial();
    }

    public void LongTutorialNextPage()
    {
        _longTutorial.NextPage();
    }
}