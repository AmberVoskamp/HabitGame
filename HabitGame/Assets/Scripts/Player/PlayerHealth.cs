using System;
using UnityEngine;

/// <summary>
/// This player health script is derived from health
/// The health of the player isn't hearts but the time they have left
/// </summary>

public class PlayerHealth : Health
{
    public static PlayerHealth Instance;

    [SerializeField] private Attack m_playerAttack;

    private CountDown _countdown;

    private Camera _mainCamera;
    private bool _isPlaying;

    protected override void Start()
    {
        base.Start();
        Instance = this;
        _mainCamera = Camera.main;
    }

    public void ActivateAttack()
    {
        m_playerAttack.BossRoom();
    }

    public void SetData(float time, CountDown countdown)
    {
        SetHealth(time);
        _countdown = countdown;
        _isPlaying = true;
    }

    public override void TakeDamage(float damage, DamageType type)
    {
        if (_isTakingDamage || m_health <= 0)
        {
            return;
        }

        StartCoroutine(Damage());

        Action action = () =>
        {
            base.TakeDamage(damage, type);
            //Update current counter
            _countdown.UpdateTimer(_currentHealth);
        };

        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(transform.position);
        _countdown.LoseTime(m_damageTime, screenPosition, action);
    }

    //If we have started, the timer will go down
    private void Update()
    {
        if (!_isPlaying || _currentHealth <= 0)
        {
            return;
        }

        _currentHealth -= Time.deltaTime;
        _countdown.UpdateTimer(_currentHealth);
    }
}
