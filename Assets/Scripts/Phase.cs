using UnityEngine;

public class Phase : MonoBehaviour
{
    [SerializeField] private PlayerSpawnpoint m_playerSpawnpoint;

    [SerializeField] private SwitchPhase _mainEntrance;

    public SwitchPhase MainEntrance
    {
        get { return _mainEntrance; }
    }

    public PlayerSpawnpoint Spawnpoint
    {
        get { return m_playerSpawnpoint; }
    }

    private void Reset()
    {
        m_playerSpawnpoint = transform.GetComponentInChildren<PlayerSpawnpoint>(true);
    }
}
