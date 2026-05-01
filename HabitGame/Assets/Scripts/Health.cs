using System.Collections;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Health is the script is an overall script for anything with health
/// For example the player health and boss health 
/// </summary>

public class Health : MonoBehaviour
{
    [SerializeField] protected float HealthAmount;
    [SerializeField] protected float DamageTime;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Color _takeDamageColor;

    private Color _basicColor;
    private float _maxHealth;
    protected bool IsTakingDamage;
    protected float CurrentHealth;
    protected ConfigManager ConfigManager;

    public float GetCurrentHealth => CurrentHealth;

    protected virtual void Start()
    {
        _basicColor = _sprite.color;
        if (CurrentHealth == 0)
        {
            SetHealth(HealthAmount);
        }

        ConfigManager = ConfigManager.Instance;
    }

    public void SetHealth(float health)
    {
        CurrentHealth = _maxHealth = health;
    }

    public virtual void TakeDamage(float damage, DamageType type)
    {
        CurrentHealth -= damage;
        CurrentHealth = math.clamp(CurrentHealth, 0, _maxHealth);
        ConfigManager?.Hit(damage, type);
    }

    public bool CanTakeDamage()
    {
        return !IsTakingDamage && _maxHealth > 0;
    }

    protected IEnumerator Damage()
    {
        IsTakingDamage = true;
        _sprite.color = _takeDamageColor;
        yield return new WaitForSeconds(DamageTime);
        _sprite.color = _basicColor;
        IsTakingDamage = false;
    }
}

public enum DamageType
{
    Spike,
    Boss,
    Player,
    Other
}