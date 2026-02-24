using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private float m_timeBetweenAttacks;
    [SerializeField] private Projectle m_projectlePrefab;
    [SerializeField] private PlayerHealth m_player;

    private Coroutine _coroutine;

    //Activated on trigger boss room
    public void BossActivate()
    {
        SpawnProjectle();
    }

    private void SpawnProjectle()
    {
        Projectle projectle = Instantiate(m_projectlePrefab, transform);
        projectle.MoveTo(m_player.transform.position);
        _coroutine = StartCoroutine(HelperWait.ActionAfterWait(m_timeBetweenAttacks, SpawnProjectle));
    }

    public void StopProjectles()
    {
        StopCoroutine(_coroutine);
    }
}
