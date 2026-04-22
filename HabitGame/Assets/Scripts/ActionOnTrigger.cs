using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script is to trigger a action 
/// </summary>

[System.Serializable]
public class PlayerEvent : UnityEvent<PlayerMovement> { }

[RequireComponent(typeof(BoxCollider2D))]
public class ActionOnTrigger : MonoBehaviour
{
    [SerializeField] private PlayerEvent _action;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            _action.Invoke(player);
        }
    }
}
