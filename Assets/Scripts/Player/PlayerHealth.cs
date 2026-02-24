using System;
using UnityEngine;

/// <summary>
/// This player health script is derived from health
/// The health of the player isn't hearts but the time they have left
/// </summary>

public class PlayerHealth : Health
{
    public static PlayerHealth Instance;

    [SerializeField] private CountDown m_countdown;

    private Camera _mainCamera;
    private bool _isPlaying;

    public float Health { get { return m_health; } }

    protected override void Start()
    {
        base.Start();
        Instance = this;
        _mainCamera = Camera.main;
        m_countdown.gameObject.SetActive(true);
        _isPlaying = true;
    }

    public override void TakeDamage(float damage)
    {
        if (_isTakingDamage || m_health <= 0)
        {
            return;
        }

        StartCoroutine(Damage());

        Action action = () =>
        {
            base.TakeDamage(damage);
            //Update current counter
            m_countdown.UpdateTimer(_currentHealth);
        };

        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(transform.position);
        m_countdown.LoseTime(m_damageTime, screenPosition, action);
    }

    //If we have started, the timer will go down
    private void Update()
    {
        if (!_isPlaying || _currentHealth <= 0)
        {
            return;
        }

        _currentHealth -= Time.deltaTime;
        m_countdown.UpdateTimer(_currentHealth);
    }
}
