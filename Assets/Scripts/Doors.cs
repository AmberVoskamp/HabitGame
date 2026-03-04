using UnityEngine;

/// <summary>
/// The Doors script is for the trigger to open the doors and open the minigame
/// </summary>

public class Doors : MonoBehaviour
{
    [SerializeField] private MinigamePopup m_popup;
    [SerializeField] private Tutorial m_tutorial;

    [Header("Doors")]
    [SerializeField] private SpriteRenderer m_leftDoor;
    [SerializeField] private SpriteRenderer m_rightDoor;
    [SerializeField] private Sprite m_leftDoorOpen;
    [SerializeField] private Sprite m_rightDoorOpen;
    [SerializeField] private BoxCollider2D m_doorCollider;

    private bool _isInRange;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_isInRange && col.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            _isInRange = true;

            m_tutorial.ShowTutorial();

            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        m_leftDoor.sprite = m_leftDoorOpen;
        m_rightDoor.sprite = m_rightDoorOpen;
    }

    public void OpenMinigamePopup()
    {
        if (_isInRange)
        {
            m_popup.ShowPopup(true);
        }
    }
}
