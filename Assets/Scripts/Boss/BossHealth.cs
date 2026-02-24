using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BossAttack))]
public class BossHealth : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private CountDown m_countDown;

    [SerializeField] private float m_health;
    [SerializeField] private float m_damageTime;
    [SerializeField] private SpriteRenderer m_bossSprite;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Color m_takeDamageColor;
    [SerializeField] private Slider m_healthSlider;

    private bool _isTakingDamage;
    private Color _basicColor;
    private BossAttack _attack;
    private float _currentHealth;

    private void Start()
    {
        _attack = this.GetComponent<BossAttack>();
        _basicColor = m_bossSprite.color;
        _currentHealth = m_health;
        SetHealthSlider();
    }

    public void StartBossBattle()
    {
        _attack.BossActivate();

        m_gameManager.EnterBossRoom();
    }

    public void TakeDamage(float damage)
    {
        if (_isTakingDamage || m_health <= 0)
        {
            return;
        }
        _currentHealth -= damage;
        _currentHealth = math.clamp(_currentHealth, 0, m_health);
        SetHealthSlider();

        if (_currentHealth <= 0)
        {
            //Boss dies you win
            _attack.StopProjectles();
            m_animator.SetTrigger("Dead");
            float animationLenght = m_animator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(HelperWait.ActionAfterWait(animationLenght, EndGame));
            return;
        }

        StartCoroutine(Damage());
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

    IEnumerator Damage()
    {
        _isTakingDamage = true;
        m_bossSprite.color = m_takeDamageColor;
        yield return new WaitForSeconds(m_damageTime);
        m_bossSprite.color = _basicColor;
        _isTakingDamage = false;
    }
}
