using JetBrains.Annotations;
using UnityEngine;

public class CircleAttack : BossAttack
{
    [SerializeField] private int m_circleProjectilsCount;
    [SerializeField] private float m_radius = 5f;


    public override void Attack(PlayerHealth player)
    {
        for (int i = 0; i < m_circleProjectilsCount; i++)
        {
            // Calculate angle in radians
            float angle = i * Mathf.PI * 2 / m_circleProjectilsCount;

            // Convert polar coordinates to Cartesian (x, y)
            float x = Mathf.Cos(angle) * m_radius;
            float y = Mathf.Sin(angle) * m_radius;

            Vector3 spawnPos = new Vector3(x, y, 0) + transform.position;
            SpawnProjectle(spawnPos);
        }
    }
}
