using UnityEngine;

/// <summary>
/// The colorful choice passage that damages the player
/// </summary>
public class Passage : DamageObject
{
    protected override bool HitPlayer(Collider2D collider, DamageType type)
    {
        return base.HitPlayer(collider, DamageType.Passage);
    }
}