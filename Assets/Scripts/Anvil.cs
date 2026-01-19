using UnityEngine;

public class Anvil : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_player;
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

    public void OpenPopup()
    {
        if (_isInRange)
        {
            m_popup.ShowPopup(true);
        }
    }
}
