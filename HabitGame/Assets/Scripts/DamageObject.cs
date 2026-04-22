using UnityEngine;

/// <summary>
/// The damage object script is for all the basic values and methods 
/// of a damage object, like the spikes. 
/// </summary>

[RequireComponent(typeof(BoxCollider2D))]
public class DamageObject : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] protected SpriteRenderer SpriteRenderer;
    [SerializeField] protected BoxCollider2D BoxCollider2D;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        InCollider(collider);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        InCollider(collider);
    }

    private void InCollider(Collider2D collider)
    {
        DamageType type = DamageType.Other;
        if (gameObject.TryGetComponent<Spikes>(out _))
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

        playerHealth.TakeDamage(_damage, type);
        return true;
    }

    private void Reset()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }
}
