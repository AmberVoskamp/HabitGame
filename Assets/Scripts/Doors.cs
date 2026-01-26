using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_player;
    [SerializeField] private UIManager m_uiManager;
    [SerializeField] private MinigamePopup m_popup;

    private bool _isInRange;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_isInRange && col.gameObject == m_player.gameObject)
        {
            _isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (_isInRange && col.gameObject == m_player.gameObject)
        {
            _isInRange = false;
        }
    }

    public void OpenEndPopup()
    {
        if (_isInRange)
        {
            m_uiManager.EndGame(true);
            Destroy(m_player.gameObject);
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
