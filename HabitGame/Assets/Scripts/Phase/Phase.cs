using UnityEngine;
public class Phase : MonoBehaviour
{
    [SerializeField] private Phases _phase;
    [SerializeField] private WalkData _walkData;
    [SerializeField] private BossHealth _boss;

    public GameManager GameManager;
    [SerializeField] public SwitchPhase MainEntrance;
    [SerializeField] public PlayerSpawnpoint Spawnpoint;

    public Phase NextPhase => GameManager.NextPhase();

    private void OnEnable()
    {
        // removed walkdata from here
    }

    public void OnPhaseStarted()
    {
        Debug.Log($"OnPhaseStarted called for phase, GameManager: {GameManager}");
        _walkData?.Record(true, PlayerHealth.Instance);
        if (GameManager != null)
        {
            GameManager.ShowPhaseTutorial(_phase);
        }
    }
    
    public bool BossRoom(out BossHealth boss)
    {
        boss = _boss;
        return _phase == Phases.Phase3;
    }

    public void ExitPhase()
    {
        GameManager.ExitPhase(_phase);
        EndPhase();
    }

    public void EndPhase()
    {
        _walkData?.Record(false);
    }

    public void ShowTutorial(string tutorialText)
    {
        GameManager.ShowTutorial(tutorialText);
    }

    private void Reset()
    {
        Spawnpoint = transform.GetComponentInChildren<PlayerSpawnpoint>(true);
    }
}