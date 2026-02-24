using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int m_damage;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Animator m_animator;
    [SerializeField] private BoxCollider2D m_boxCollider2D;

    private bool m_spikes;

    void Start()
    {
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
        HitPlayer(collider);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        HitPlayer(collider);
    }

    IEnumerator WaitForAnimation(float animationTime, bool colliderActive)
    {
        yield return new WaitForSeconds(animationTime);
        m_boxCollider2D.enabled = colliderActive;
    }

    private void HitPlayer(Collider2D collider)
    {
        if (!m_spikes || !collider.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            return;
        }
        //When hit up the hitcount in leveldata
        playerHealth.TakeDamage(m_damage);
    }
}
