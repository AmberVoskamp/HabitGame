using UnityEngine;

/// <summary>
/// Projectiles will attack the player and do damage once they hit
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
public class Projectle : DamageObject
{
    [SerializeField] private float m_liveTime;
    [SerializeField] private float m_speed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        HitPlayer(collider);
    }

    public void MoveTo(Vector3 pos)
    {
        Vector3 direction = (pos - transform.position).normalized;

        _rigidbody.linearVelocity = direction * m_speed;

        StartCoroutine(HelperWait.ActionAfterWait(m_liveTime, DestroyProjectile));
    }

    protected override bool HitPlayer(Collider2D collider)
    {
        bool hitPlayer = base.HitPlayer(collider);

        if (hitPlayer)
        {
            DestroyProjectile();
        }

        return hitPlayer;
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
