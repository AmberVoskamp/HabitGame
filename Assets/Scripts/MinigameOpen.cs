using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MinigameOpen : MonoBehaviour
{
    [SerializeField] private MinigamePopup m_popup;

    private bool _isInRange;
    private PlayerMovement m_player;

    void OnTriggerEnter2D(Collider2D col)
    {
        IsInRange(true, col);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        IsInRange(false, col);
    }

    private void IsInRange(bool isInRange, Collider2D col)
    {
        if (_isInRange != isInRange && col.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            _isInRange = isInRange;
            player.IsInMinigameRange = isInRange;
        }
    }
}
