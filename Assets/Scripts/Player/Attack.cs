using UnityEngine;

/// <summary>
/// Player attack script is so that the player can attack the boss
/// </summary>

public class Attack : MonoBehaviour
{
    [SerializeField] private Animator m_attackAnimator;
    [SerializeField] private float m_doDamage;
    [SerializeField] private float m_timeBetweenAttack;

    private bool _isInBossRoom;
    private bool _bossInRange;
    private BossHealth _bossHealth;

    //Gets triggerd on input
    public void DoAttack()
    {
        if (!_isInBossRoom)
        {
            return;
        }

        m_attackAnimator.SetTrigger("Attack");

        if (_bossInRange && _bossHealth != null)
        {
            //Damage boss
            _bossHealth.TakeDamage(m_doDamage, DamageType.Player);
        }
    }

    //Gets triggerd when you enter the boss room
    public void BossRoom()
    {
        _isInBossRoom = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<BossHealth>(out BossHealth bossHealth))
        {
            _bossInRange = true;

            if (_bossHealth == null)
            {
                _bossHealth = bossHealth;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<BossHealth>(out BossHealth bossHealth))
        {
            _bossInRange = false;
        }
    }
}
