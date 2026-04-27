using UnityEngine;

/// <summary>
/// These are the individual spikes which deprives from DamageObjects
/// If the player hits the spikes when they are up they take damage
/// </summary>

public class Spikes : DamageObject
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite _spriteDown;
    [SerializeField] Sprite _spriteUp;

    private bool _spikes;

    private void Start()
    {
        BoxCollider2D.enabled = false;
    }

    public void SpikeUp(bool up)
    {
        if (_spikes == up)
        {
            return;
        }

        _spikes = up;
        _spriteRenderer.sprite = up ? _spriteUp : _spriteDown;

        BoxCollider2D.enabled = up;
    }
}
