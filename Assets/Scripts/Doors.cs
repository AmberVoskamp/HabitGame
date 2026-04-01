using UnityEngine;

/// <summary>
/// The Doors script is for the trigger to open the doors and open the minigame
/// </summary>

public class Doors : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] private SpriteRenderer m_leftDoor;
    [SerializeField] private SpriteRenderer m_rightDoor;
    [SerializeField] private Sprite m_leftDoorOpen;
    [SerializeField] private Sprite m_rightDoorOpen;
    [SerializeField] private BoxCollider2D m_doorCollider;

    private bool _isInRange;
    private bool _doorOpen;

    void OnTriggerEnter2D(Collider2D col)
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
                ConfigManager.Instance.TimeLeftDoorOpens(playerHealth.CurrentHealth);
            }
        }
    }

    public void OpenDoor()
    {
        _doorOpen = true;
        m_leftDoor.sprite = m_leftDoorOpen;
        m_rightDoor.sprite = m_rightDoorOpen;
    }
}
