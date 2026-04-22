using UnityEngine;

public class CircleAttack : BossAttack
{
    [SerializeField] private int _circleProjectilsCount;
    [SerializeField] private float _radius = 5f;

    public override void Attack(PlayerHealth player)
    {
        for (int i = 0; i < _circleProjectilsCount; i++)
        {
            // Calculate angle in radians
            float angle = i * Mathf.PI * 2 / _circleProjectilsCount;

            // Convert polar coordinates to Cartesian (x, y)
            float x = Mathf.Cos(angle) * _radius;
            float y = Mathf.Sin(angle) * _radius;

            Vector3 spawnPos = new Vector3(x, y, 0) + transform.position;
            SpawnProjectle(spawnPos);
        }
    }
}
