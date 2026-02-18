using UnityEngine;
using UnityEngine.Events;

public class ActionOnTrigger : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_player;
    [SerializeField] private UnityEvent m_action;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == m_player.gameObject)
        {
            m_action.Invoke();
        }
    }
}
