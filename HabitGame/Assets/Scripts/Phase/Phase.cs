using UnityEngine;

public class Phase : MonoBehaviour
{
    [SerializeField] private Phases _phase;

    //not all phases will have these
    [SerializeField] private WalkData _walkData;
    [SerializeField] private BossHealth _boss;

    public GameManager GameManager;

    [SerializeField]
    public SwitchPhase MainEntrance;

    [SerializeField]
    public PlayerSpawnpoint Spawnpoint;

    public Phase NextPhase => GameManager.NextPhase();

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
        GameManager.ExitPhase(_phase);
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
