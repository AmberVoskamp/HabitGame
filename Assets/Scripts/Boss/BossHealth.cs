using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BossHealth is derived from Health and takes care of 
/// showcasing the health and what happens if the boss is dead
/// </summary>

[RequireComponent(typeof(BossAttack))]
public class BossHealth : Health
{
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private PlayerHealth m_playerHealth;

    [SerializeField] private Animator m_animator;
    [SerializeField] private Slider m_healthSlider;

    private BossAttack _attack;

    protected override void Start()
    {
        base.Start();
        _attack = this.GetComponent<BossAttack>();
        SetHealthSlider();
    }

    //Activated on trigger boss room
    public void StartBossBattle()
    {
        _attack.BossActivate();

        m_gameManager.EnterBossRoom();
    }

    public override void TakeDamage(float damage)
    {
        if (!CanTakeDamage())
        {
            return;
        }

        StartCoroutine(Damage());
        base.TakeDamage(damage);

        SetHealthSlider();

        #region Boss died
        if (_currentHealth <= 0)
        {
            //Boss dies you win
            _attack.StopProjectles();
            m_animator.SetTrigger("Dead");
            float animationLenght = m_animator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(HelperWait.ActionAfterWait(animationLenght, EndGame));
            return;
        }
        #endregion
    }

    private void EndGame()
    {
        m_gameManager.EndGame(true, m_playerHealth.CurrentHealth);
    }

    private void SetHealthSlider()
    {
        float healthPercentage = _currentHealth / m_health;
        m_healthSlider.value = healthPercentage;
    }
}
