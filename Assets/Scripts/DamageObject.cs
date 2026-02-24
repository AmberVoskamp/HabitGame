using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DamageObject : MonoBehaviour
{
    [SerializeField] private int m_damage;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] protected BoxCollider2D m_boxCollider2D;

    void OnTriggerEnter2D(Collider2D collider)
    {
        HitPlayer(collider);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        HitPlayer(collider);
    }

    protected virtual bool HitPlayer(Collider2D collider)
    {
        if (!collider.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            return false;
        }

        playerHealth.TakeDamage(m_damage);
        return true;
    }

    private void Reset()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_boxCollider2D = GetComponent<BoxCollider2D>();
    }
}
