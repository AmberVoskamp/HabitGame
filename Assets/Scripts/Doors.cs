using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_player;
    [SerializeField] private GameObject m_endPopup;

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
            m_endPopup.SetActive(true);
            Destroy(m_player.gameObject);
        }
    }
}
