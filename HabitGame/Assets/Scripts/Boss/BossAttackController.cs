using System;
using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    [SerializeField] private float m_waitForStart;
    [SerializeField] private AttackPaternHealth[] m_attackPaternHealth;

    private int m_attackPaternHealthIndex;
    private int m_attackIndex;
    private Coroutine m_attackCoroutine;
    private PlayerHealth m_player;
    private BossHealth m_bossHealth;

    #region structs
    [Serializable] 
    public struct AttackPaternHealth
    {
        public float health;
        public BossAttackPatern[] attackPatern;
    }

    [Serializable]
    public struct BossAttackPatern
    {
        public BossAttack attack;
        public float timeBeforeNextAttack;
    }
    #endregion

    //Activated once the player enters the boss room
    public void BossActivate(PlayerHealth player, BossHealth bossHealth)
    {
        m_player = player;
        m_bossHealth = bossHealth;
        SetCurrentAttackPatternIndex(bossHealth.CurrentHealth);
        WaitForAttack(m_waitForStart);
    }

    private void SetCurrentAttackPatternIndex(float bossHealth)
    {
        m_attackPaternHealthIndex = 0;
        for (int i = 0; i < m_attackPaternHealth.Length; i++)
        {
            if (bossHealth <= m_attackPaternHealth[i].health)
            {
                m_attackPaternHealthIndex = i;
            }
            else
            {
                break;
            }
        }
    }

    private void Attack()
    {
        m_attackCoroutine = null;
        BossAttackPatern[] currentAttackPatern = m_attackPaternHealth[m_attackPaternHealthIndex].attackPatern;
        if (m_attackIndex >= currentAttackPatern.Length)
        {
            m_attackIndex = 0;
            SetCurrentAttackPatternIndex(m_bossHealth.CurrentHealth);
            Attack();
            return;
        }

        BossAttackPatern bossAttack = currentAttackPatern[m_attackIndex];
        m_attackIndex++;
        bossAttack.attack.Attack(m_player);
        WaitForAttack(bossAttack.timeBeforeNextAttack);
    }

    private void WaitForAttack(float time)
    {
       m_attackCoroutine = StartCoroutine(HelperWait.ActionAfterWait(time, Attack));
    }

    public void StopAttacks()
    {
        StopCoroutine(m_attackCoroutine);
    }
}
