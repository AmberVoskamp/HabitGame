using System;
using UnityEngine;

/*
 * This player health script will take care of taking damage
 * The health of the player isn't hearts but the time they have left
 */
public class PlayerHealth : Health
{
    public static PlayerHealth Instance;

    [SerializeField] private CountDown m_countdown;

    private Camera _mainCamera;
    private bool _isPlaying;

    public float Health { get { return m_health; } }

    private void Awake()
    {
        Instance = this;
        _mainCamera = Camera.main;
        gameObject.SetActive(false);
    }

    public void StartGame()
    {
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
