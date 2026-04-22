using UnityEngine;

/// <summary>
/// This player health script is derived from health
/// The health of the player isn't hearts but the time they have left
/// </summary>

public class PlayerHealth : Health
{
    public static PlayerHealth Instance;

    [SerializeField] private Attack _playerAttack;

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
        _playerAttack.BossRoom();
    }

    public void SetData(float time, CountDown countdown)
    {
        SetHealth(time);
        _countdown = countdown;
        _isPlaying = true;
    }

    public override void TakeDamage(float damage, DamageType type)
    {
        if (IsTakingDamage || HealthAmount <= 0)
        {
            return;
        }

        _ = StartCoroutine(Damage());

        void action()
        {
            base.TakeDamage(damage, type);
            //Update current counter
            _countdown.UpdateTimer(CurrentHealth);
        }

        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(transform.position);
        _countdown.LoseTime(DamageTime, screenPosition, action);
    }

    //If we have started, the timer will go down
    private void Update()
    {
        if (!_isPlaying || CurrentHealth <= 0)
        {
            return;
        }

        CurrentHealth -= Time.deltaTime;
        _countdown.UpdateTimer(CurrentHealth);
    }
}
