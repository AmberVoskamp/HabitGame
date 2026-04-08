using UnityEngine;

/// <summary>
/// This boss attack script will spawn projectles that attack the player
/// </summary>

public class BossAttack : MonoBehaviour
{
    [SerializeField] private Projectle m_projectlePrefab;

    public virtual void Attack(PlayerHealth player)
    {
        Debug.Log("This is the bass boss attack");
    }

    protected void SpawnProjectle(Vector3 moveTo)
    {
        Projectle projectle = Instantiate(m_projectlePrefab, transform);
        projectle.MoveTo(moveTo);
    }
}
