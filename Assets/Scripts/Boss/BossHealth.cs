using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BossAttack))]
public class BossHealth : Health
{
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private CountDown m_countDown;

    [SerializeField] private Animator m_animator;
    [SerializeField] private Slider m_healthSlider;

    private BossAttack _attack;

    protected override void Start()
    {
        base.Start();
        _attack = this.GetComponent<BossAttack>();
        SetHealthSlider();
    }

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
        m_gameManager.EndGame(true, m_countDown.CurrentTime);
    }

    private void SetHealthSlider()
    {
        float healthPercentage = _currentHealth / m_health;
        m_healthSlider.value = healthPercentage;
    }
}
