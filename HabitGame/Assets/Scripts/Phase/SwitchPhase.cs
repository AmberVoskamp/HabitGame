using UnityEngine;

[RequireComponent(typeof(ActionOnTrigger))]
public class SwitchPhase : MonoBehaviour
{
    [Header("Health Parameters")]
    [SerializeField] private float _moveForwardAmount;

    private Phase m_phase;
    private bool m_isEntering;

    private void Start()
    {
        SetPhase();
    }

    public void GoToNextPhase(PlayerMovement playerMovement)
    {
        if (m_isEntering)
        {
            return;
        }
        m_isEntering = true;
        //TODO Screen go black (can do later)

        //Load in next phase
        Phase next = m_phase.NextPhase;
        if (next == null)
        {
            return;
        }

        Phase nextPhase = Instantiate(next);
        nextPhase.GameManager = m_phase.GameManager;

        //Send player to the entrance of the next phase
        nextPhase.MainEntrance.PlayerEnter(playerMovement);
        m_phase.ExitPhase();

        //Turn off current phase (might want to go back so keep it in the scene)
        m_phase.gameObject.SetActive(false);
    }

    public void PlayerEnter(PlayerMovement playerMovement)
    {
        SetPhase();
        if (m_phase.BossRoom(out BossHealth boss))
        {
            //TODO If it is the boss room activate the attack and boss fight
            PlayerHealth.Instance.ActivateAttack();
        }

        m_isEntering = true;

        playerMovement.Entrance(transform.position);
    }

    private void SetPhase()
    {
        if (m_phase == null)
        {
            m_phase = GetComponentInParent<Phase>();
        }
    }
}
