using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectle : MonoBehaviour
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

    private void HitPlayer(Collider2D collider)
    {
        if (!collider.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            return;
        }

        playerHealth.TakeDamage(10);
        Destroy(gameObject);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
