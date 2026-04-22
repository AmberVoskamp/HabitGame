using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BossHealth is derived from Health and takes care of 
/// showcasing the health and what happens if the boss is dead
/// </summary>

[RequireComponent(typeof(BossAttack))]
public class BossHealth : Health
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Animator _animator;
    [SerializeField] private Slider _healthSlider;

    private PlayerHealth _playerHealth;
    private BossAttackController _attack;

    protected override void Start()
    {
        base.Start();

        SetBossHealth();
        SetHealthSlider();

        _playerHealth = PlayerHealth.Instance;
        _attack = GetComponent<BossAttackController>();

        _attack.BossActivate(_playerHealth, this);
        _playerHealth.ActivateAttack();

        if (_gameManager == null)
        {
            Phase phase = GetComponentInParent<Phase>();
            _gameManager = phase.GameManager;
        }

        _gameManager.EnterBossRoom(CurrentHealth);
    }

    public void SetBossHealth()
    {
        if (ConfigManager == null)
        {
            return;
        }

        float bossHealth = ConfigManager.GetBossHealth();
        if (bossHealth == 0)
        {
            SetHealth(HealthAmount);
            return;
        }
        SetHealth(bossHealth);
    }

    public override void TakeDamage(float damage, DamageType type)
    {
        if (!CanTakeDamage())
        {
            return;
        }

        _ = StartCoroutine(Damage());
        base.TakeDamage(damage, type);

        SetHealthSlider();

        #region Boss died
        if (CurrentHealth <= 0)
        {
            //Boss dies you win
            _attack?.StopAttacks();
            _animator.SetTrigger("Dead");
            float animationLenght = _animator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(HelperWait.ActionAfterWait(animationLenght, EndGame));
            return;
        }
        #endregion
    }

    private void EndGame()
    {
        _gameManager.EndGame(true, _playerHealth.GetCurrentHealth);
    }

    private void SetHealthSlider()
    {
        float healthPercentage = CurrentHealth / HealthAmount;
        _healthSlider.value = healthPercentage;
    }
}
