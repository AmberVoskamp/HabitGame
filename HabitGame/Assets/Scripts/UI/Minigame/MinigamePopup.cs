using TMPro;
using UnityEngine;

/// <summary>
/// This minigame popup is for showing or not showing the popup
/// </summary>

public class MinigamePopup : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TMP_Text _turotial;
    [SerializeField] private UpgradeWeaponUI _upgradeWeaponUI;
    [SerializeField] private Minigame _minigameScreen;
    [Space]
    [SerializeField] private float _noTapTime;

    private bool _minigameDone;
    private float _waitTime;
    private bool _minigameActive;

    private void Start()
    {
        ShowPopup(false);
    }

    private void Update()
    {
        if (_waitTime > 0f)
        {
            _waitTime = Mathf.Max(0f, _waitTime - Time.deltaTime);
        }
    }

    public void ShowTutorial(bool show)
    {
        _turotial.gameObject.SetActive(show);
    }

    public void ShowPopup(bool show)
    {
        if (show && _minigameDone)
        {
            return;
        }

        gameObject.SetActive(show);

        if (!show)
        {
            return;
        }

        _gameManager.MiniGameData(true, _minigameDone);

        if (_minigameDone)
        {
            return;
        }
        StartMiniGame();
    }

    public void CompletedMinigame()
    {
        _gameManager.MiniGameData(true, _minigameDone);

        _minigameScreen.gameObject.SetActive(false);
        _turotial.gameObject.SetActive(false);

        _upgradeWeaponUI.gameObject.SetActive(true);
        if (PlayerHealth.Instance != null)
        {
            Vector2 upgradeDamage = PlayerHealth.Instance.PlayerAttackUpgrade;
            _upgradeWeaponUI.SetState(upgradeDamage); //Test values
        }

        ShowPopup(true);
        _minigameDone = true;
    }

    public void TapInput()
    {
        if (_waitTime > 0)
        {
            return;
        }

        _waitTime = _noTapTime;

        if (!_minigameDone)
        {
            _minigameScreen.Tap();

            return;
        }

        ShowPopup(false);
    }

    private void StartMiniGame()
    {
        if (_minigameActive)
        {
            return;
        }

        _minigameActive = true;
        _upgradeWeaponUI.gameObject.SetActive(false);
        _minigameScreen.gameObject.SetActive(true);
        _waitTime = _noTapTime;
        _minigameScreen.StartGame();
    }
}
