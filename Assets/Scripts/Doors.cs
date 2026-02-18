using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_player;
    [SerializeField] private UIManager m_uiManager;
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
        if (!_isInRange && col.gameObject == m_player.gameObject)
        {
            _isInRange = true;

            m_tutorial.ShowTutorial();

            #region Open boss door
            m_leftDoor.sprite = m_leftDoorOpen;
            m_rightDoor.sprite = m_rightDoorOpen;
            //m_doorCollider.enabled = false;
            #endregion
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (_isInRange && col.gameObject == m_player.gameObject)
        {
            _isInRange = false;
        }
    }

    public void OpenMinigamePopup()
    {
        if (_isInRange)
        {
            m_popup.ShowPopup(true);
        }
    }
}
