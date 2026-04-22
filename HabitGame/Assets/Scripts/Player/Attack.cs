using UnityEngine;

/// <summary>
/// Player attack script is so that the player can attack the boss
/// </summary>

public class Attack : MonoBehaviour
{
    [SerializeField] private Animator _attackAnimator;
    [SerializeField] private float _normalDamage;
    [SerializeField] private float _upgradeDamage;
    [SerializeField] private float _timeBetweenAttack;

    private bool _isInBossRoom;
    private bool _bossInRange;
    private BossHealth _bossHealth;
    private float _doDamage;

    private void Start()
    {
        _doDamage = _normalDamage;
    }

    public void UpgradeAttack()
    {
        _doDamage = _upgradeDamage;
    }

    //Gets triggerd on input
    public void DoAttack()
    {
        if (!_isInBossRoom)
        {
            return;
        }

        _attackAnimator.SetTrigger("Attack");

        if (_bossInRange && _bossHealth != null)
        {
            //Damage boss
            _bossHealth.TakeDamage(_doDamage, DamageType.Player);
        }
    }

    //Gets triggerd when you enter the boss room
    public void BossRoom()
    {
        _isInBossRoom = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
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

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<BossHealth>(out _))
        {
            _bossInRange = false;
        }
    }
}
