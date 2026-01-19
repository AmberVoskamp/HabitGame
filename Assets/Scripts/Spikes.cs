using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int m_damage;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Sprite m_retractSprite;
    [SerializeField] private Sprite m_spikeSprite;

    private bool m_spikes;
    private PlayerHealth m_playerHealth;

    void Start()
    {
        m_playerHealth = PlayerHealth.Instance;
    }

    public void SpikeUp(bool up)
    {
        m_spikes = up;
        m_spriteRenderer.sprite = up ? m_spikeSprite : m_retractSprite;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!m_spikes)
        {
            return;
        }
        m_playerHealth.TakeDamage(m_damage);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!m_spikes)
        {
            return;
        }
        m_playerHealth.TakeDamage(m_damage);
    }
}
