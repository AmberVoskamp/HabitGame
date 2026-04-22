using TMPro;
using UnityEngine;

/// <summary>
/// This tutorial script is to toggle and showcase the tutorial 
/// </summary>

public class Tutorial : MonoBehaviour
{
    [SerializeField] private MinigamePopup _minigamePopup;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private bool _doTutorial;

    private bool _tutorialShowing = false;
    private ConfigManager _configManager;

    private void Awake()
    {
        _configManager = ConfigManager.Instance;
        if (_configManager != null)
        {
            _doTutorial = !_configManager.Config.TutorialFinished;
        }

        _minigamePopup.ShowTutorial(_doTutorial);
        if (!_tutorialShowing)
        {
            gameObject.SetActive(false);
        }
    }

    public void ShowTutorial(string tutorialText)
    {
        if (!_doTutorial)
        {
            return;
        }

        _tutorialShowing = true;
        _text.text = tutorialText;
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    //Input UI click
    public void ContinueGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        _tutorialShowing = false;
    }
}
