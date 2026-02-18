using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : MonoBehaviour
{
    public static SceneSwitchManager Instance;
    [SerializeField] private int m_homeSceneIndex;
    [SerializeField] private int m_GameSceneIndex;

    void Start()
    {
        Instance = this;
        SwitchScene(Scenes.HomeScene);
    }

    public void SwitchScene(Scenes scene)
    {
        int sceneIndex = scene switch
        {
            Scenes.HomeScene => m_homeSceneIndex,
            Scenes.GameScene => m_GameSceneIndex,
            _ => 0,
        };

        SceneManager.LoadScene(sceneIndex);
    }
}

public enum Scenes
{
    HomeScene,
    GameScene,
}
