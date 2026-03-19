using UnityEngine;

[RequireComponent(typeof(ActionOnTrigger))]
public class SwitchPhase : MonoBehaviour
{
    [SerializeField] private Phase _nextPhase;

    [Header("Health Parameters")]
    [SerializeField] private float _moveForwardAmount;

    private Phase m_phase;
    private bool m_isEntering;

    private void Start()
    {
        m_phase = GetComponentInParent<Phase>();
    }

    public void GoToNextPhase(PlayerMovement playerMovement)
    {
        if (m_isEntering)
        {
            return;
        }
        //Screen go black (can do later)

        //Load in next phase
        Phase nextPhase = Instantiate(_nextPhase);

        Debug.Log($" nextPhase.MainEntrance {nextPhase.MainEntrance == null} playerMovement {playerMovement == null}");
        //Send player to the entrance of the next phase
        nextPhase.MainEntrance.PlayerEnter(playerMovement);

        //Turn off current phase (might want to go back so keep it in the scene)
        m_phase.gameObject.SetActive(false);

        Debug.Log("Go to next phase");
    }

    public void PlayerEnter(PlayerMovement playerMovement)
    {
        m_isEntering = true;

        playerMovement.Entrance(transform.position);
    }
}
