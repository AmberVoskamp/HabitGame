using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script is to trigger a action 
/// </summary>

[RequireComponent(typeof(BoxCollider2D))]
public class ActionOnTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent m_action;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            m_action.Invoke();
        }
    }
}
