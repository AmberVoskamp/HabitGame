using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float m_health;
    [SerializeField] protected float m_damageTime;
    [SerializeField] private SpriteRenderer m_sprite;
    [SerializeField] private Color m_takeDamageColor;

    private Color _basicColor;
    protected bool _isTakingDamage;
    protected float _currentHealth;

    protected virtual void Start()
    {
        _basicColor = m_sprite.color;
        _currentHealth = m_health;
    }

    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = math.clamp(_currentHealth, 0, m_health);
    }

    public bool CanTakeDamage()
    {
        return !_isTakingDamage && m_health > 0;
    }

    protected IEnumerator Damage()
    {
        _isTakingDamage = true;
        m_sprite.color = m_takeDamageColor;
        yield return new WaitForSeconds(m_damageTime);
        m_sprite.color = _basicColor;
        _isTakingDamage = false;
    }
}
