using UnityEngine;

/// <summary>
/// The Doors script is for the trigger to open the doors and open the minigame
/// </summary>

public class Doors : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] private SpriteRenderer _leftDoor;
    [SerializeField] private SpriteRenderer _rightDoor;
    [SerializeField] private Sprite _leftDoorOpen;
    [SerializeField] private Sprite _rightDoorOpen;
    [SerializeField] private BoxCollider2D _doorCollider;

    private bool _isInRange;
    private bool _doorOpen;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_doorOpen)
        {
            return;
        }

        if (!_isInRange && col.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            _isInRange = true;
            OpenDoor();

            if (ConfigManager.Instance != null)
            {
                ConfigManager.Instance.TimeLeftDoorOpens(playerHealth.GetCurrentHealth);
            }
        }
    }

    public void OpenDoor()
    {
        _doorOpen = true;
        _leftDoor.sprite = _leftDoorOpen;
        _rightDoor.sprite = _rightDoorOpen;
    }
}
