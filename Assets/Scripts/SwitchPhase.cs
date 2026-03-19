using UnityEngine;

[RequireComponent(typeof(ActionOnTrigger))]
public class SwitchPhase : MonoBehaviour
{
    [SerializeField] private Phase _nextPhase;

    [Header("Health Parameters")]
    [SerializeField] private float _moveForwardAmount;

    private bool m_isEntering;

    public void GoToNextPhase(PlayerMovement playerMovement)
    {
        if (m_isEntering)
        {
            return;
        }
        //Screen go black (can do later)

        //Load in next phase
        Phase nextPhase = Instantiate(_nextPhase);

        //Send player to the entrance of the next phase
        nextPhase.MainEntrance.PlayerEnter(playerMovement);
        Debug.Log("Go to next phase");
    }

    public void PlayerEnter(PlayerMovement playerMovement)
    {
        m_isEntering = true;

        playerMovement.Entrance(transform.position);
    }
}
