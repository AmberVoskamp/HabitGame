/// <summary>
/// This boss attack shoots a projectle at the players location
/// </summary>

public class StraightShot : BossAttack
{
    public override void Attack(PlayerHealth player)
    {
        SpawnProjectle(player.transform.position);
    }
}
