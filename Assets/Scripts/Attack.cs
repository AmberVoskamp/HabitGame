using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Animator m_attackAnimator;

    private bool _isInBossRoom;
    private bool _bossInRange;
    private BossHealth _bossHealth;

    public void DoAttack()
    {
        if (!_isInBossRoom)
        {
            return;
        }

        m_attackAnimator.SetTrigger("Attack");

        if (_bossInRange)
        {
            //Damage boss
        }
    }

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
            //m_action.Invoke();
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
