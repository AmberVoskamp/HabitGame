using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MinigameOpen : MonoBehaviour
{
    private bool _hasEntered;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_hasEntered) return;
        if (!col.gameObject.TryGetComponent<PlayerMovement>(out _)) return;

        _hasEntered = true;
        UIManager.Instance?.ShowMinigame(true);
    }
}