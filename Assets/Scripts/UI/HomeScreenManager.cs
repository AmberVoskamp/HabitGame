using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreenManager : MonoBehaviour
{
    [SerializeField] private Button m_playButton;

    private void Start()
    {
        m_playButton.onClick.AddListener(GameScene);
    }

    private void GameScene()
    {
        SceneSwitchManager.Instance.SwitchScene(Scenes.GameScene);
    }
}
