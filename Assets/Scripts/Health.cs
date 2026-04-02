using System.Collections;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Health is the script is an overall script for anything with health
/// For example the player health and boss health 
/// </summary>

public class Health : MonoBehaviour
{
    [SerializeField] protected float m_health;
    [SerializeField] protected float m_damageTime;
    [SerializeField] private SpriteRenderer m_sprite;
    [SerializeField] private Color m_takeDamageColor;

    private Color _basicColor;
    protected bool _isTakingDamage;
    protected float _currentHealth;
    protected ConfigManager _configManager;

    public float CurrentHealth { get { return _currentHealth; } }

    protected virtual void Start()
    {
        _basicColor = m_sprite.color;
        if (_currentHealth == 0)
        {
            SetHealth(m_health);
        }

        _configManager = ConfigManager.Instance;
    }

    public void SetHealth(float health)
    {
        _currentHealth = health;
    }

    public virtual void TakeDamage(float damage, DamageType type)
    {
        _currentHealth -= damage;
        _currentHealth = math.clamp(_currentHealth, 0, m_health);
        _configManager?.Hit(damage, type);
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

public enum DamageType
{
    Spike,
    Boss,
    Player,
    Other
}