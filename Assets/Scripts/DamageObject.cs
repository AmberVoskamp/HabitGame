using UnityEngine;

/// <summary>
/// The damage object script is for all the basic values and methods 
/// of a damage object, like the spikes. 
/// </summary>

[RequireComponent(typeof(BoxCollider2D))]
public class DamageObject : MonoBehaviour
{
    [SerializeField] private int m_damage;
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    [SerializeField] protected BoxCollider2D m_boxCollider2D;

    void OnTriggerEnter2D(Collider2D collider)
    {
        InCollider(collider);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        InCollider(collider);
    }

    private void InCollider(Collider2D collider)
    {
        DamageType type = DamageType.Other;
        if (gameObject.TryGetComponent<Spikes>(out Spikes spike))
        {
            type = DamageType.Spike;
        }
        HitPlayer(collider, type);
    }

    protected virtual bool HitPlayer(Collider2D collider, DamageType type)
    {
        if (!collider.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            return false;
        }

        playerHealth.TakeDamage(m_damage, type);
        return true;
    }

    private void Reset()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_boxCollider2D = GetComponent<BoxCollider2D>();
    }
}
