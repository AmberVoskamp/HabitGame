using UnityEngine;

/// <summary>
/// This boss attack script will spawn projectles that attack the player
/// </summary>

public class BossAttack : MonoBehaviour
{
    [SerializeField] private float m_timeBetweenAttacks;
    [SerializeField] private Projectle m_projectlePrefab;
    [SerializeField] private PlayerHealth m_player;

    private Coroutine _coroutine;

    //Activated once the player enters the boss room
    public void BossActivate()
    {
        SpawnProjectle();
    }

    //Every [m_timeBetweenAttacks] seconds a projectile will spawn and attack the player
    private void SpawnProjectle()
    {
        Projectle projectle = Instantiate(m_projectlePrefab, transform);
        projectle.MoveTo(m_player.transform.position);
        _coroutine = StartCoroutine(HelperWait.ActionAfterWait(m_timeBetweenAttacks, SpawnProjectle));
    }

    //Stops the projectiles from spawning 
    public void StopProjectles()
    {
        StopCoroutine(_coroutine);
    }
}
