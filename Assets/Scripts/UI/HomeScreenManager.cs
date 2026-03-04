using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The home screen manager handels the home screen UI inputs
/// </summary>

public class HomeScreenManager : MonoBehaviour
{
    [SerializeField] private Button m_playButton;

    private void Start()
    {
        m_playButton.onClick.AddListener(GameScene);
        ///m_playButton.hove
    }

    private void GameScene()
    {
        SceneSwitchManager.Instance.SwitchScene(Scenes.GameScene);
    }
}
