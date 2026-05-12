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

    [SerializeField] private TMP_Text _upgradeText;
    [SerializeField] private GameObject _swordImage;

    private bool _minigameDone;
    private float _waitTime;
    private bool _minigameActive;
    private bool _upgradeShown;

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
        _minigameDone = true;
        _upgradeShown = true;
        _gameManager.MiniGameData(true, _minigameDone);
        _minigameScreen.gameObject.SetActive(false);
        _turotial.gameObject.SetActive(false);

        bool isTestLevel = ConfigManager.Instance != null && ConfigManager.Instance.Config.IsInTestPhase;
        if (isTestLevel)
        {
            _upgradeText.text = "No upgrade available";
            _swordImage.SetActive(false);
        }
        else
        {
            _upgradeText.text = "Your weapon has been upgraded!";
            _swordImage.SetActive(true);
            if (PlayerHealth.Instance != null)
            {
                Vector2 upgradeDamage = PlayerHealth.Instance.PlayerAttackUpgrade;
                _upgradeWeaponUI.SetState(upgradeDamage);
                PlayerHealth.Instance.UpgradeAttack();
            }
        }

        _upgradeWeaponUI.gameObject.SetActive(true);
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }
    
    public void TapInput()
    {
        if (_waitTime > 0) return;
        _waitTime = _noTapTime;

        if (!_minigameDone)
        {
            _minigameScreen.Tap();
            return;
        }

        if (!_upgradeShown)
        {
            return; // wait until upgrade screen is shown
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
