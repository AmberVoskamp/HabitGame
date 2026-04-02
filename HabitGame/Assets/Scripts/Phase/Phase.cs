using UnityEngine;

public class Phase : MonoBehaviour
{
    [SerializeField] private Phases _phase;

    //not all phases will have these
    [SerializeField] private PlayerSpawnpoint _playerSpawnpoint;
    [SerializeField] private SwitchPhase _mainEntrance;
    [SerializeField] private WalkData _walkData;
    [SerializeField] private BossHealth _boss;

    private GameManager m_gameManager;

    public GameManager GameManager
    {
        get { return m_gameManager; } 
        set { m_gameManager = value; }
    }

    public SwitchPhase MainEntrance
    {
        get { return _mainEntrance; }
    }

    public PlayerSpawnpoint Spawnpoint
    {
        get { return _playerSpawnpoint; }
    }

    public Phase NextPhase
    {
        get { return m_gameManager.NextPhase(); }
    }

    private void OnEnable()
    {
        _walkData?.Record(true, PlayerHealth.Instance);
    }

    public bool BossRoom(out BossHealth boss)
    {
        boss = _boss;
        return _phase == Phases.Phase3;
    }

    public void ExitPhase()
    {
        m_gameManager.ExitPhase(_phase);
        _walkData?.Record(false);
    }

    private void Reset()
    {
        _playerSpawnpoint = transform.GetComponentInChildren<PlayerSpawnpoint>(true);
    }
}
