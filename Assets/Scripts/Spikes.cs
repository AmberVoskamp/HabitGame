using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int m_damage;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Animator m_animator;
    [SerializeField] private BoxCollider2D m_boxCollider2D;

    private bool m_spikes;
    private PlayerHealth m_playerHealth;

    void Start()
    {
        m_playerHealth = PlayerHealth.Instance;
        m_boxCollider2D.enabled = false;
    }

    public void SpikeUp(bool up)
    {
        if (m_spikes == up)
        {
            return;
        }

        m_spikes = up;
        string animation = up ? "Open" : "Close";
        m_animator.SetTrigger(animation);

        if (up)
        {
            float animationTime = m_animator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(WaitForAnimation(animationTime, up));
            return;
        }

        m_boxCollider2D.enabled = up;
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

    IEnumerator WaitForAnimation(float animationTime, bool colliderActive)
    {
        yield return new WaitForSeconds(animationTime);
        m_boxCollider2D.enabled = colliderActive;
    }
}
