using System.Collections;
using UnityEngine;

/// <summary>
/// These are the individual spikes which deprives from DamageObjects
/// If the player hits the spikes when they are up they take damage
/// </summary>

public class Spikes : DamageObject
{
    [SerializeField] private Animator _animator;

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
        string animation = up ? "Open" : "Close";
        _animator.SetTrigger(animation);

        if (up)
        {
            float animationTime = _animator.GetCurrentAnimatorStateInfo(0).length;
            _ = StartCoroutine(WaitForAnimation(animationTime, up));
            return;
        }

        BoxCollider2D.enabled = up;
    }

    private IEnumerator WaitForAnimation(float animationTime, bool colliderActive)
    {
        yield return new WaitForSeconds(animationTime);
        BoxCollider2D.enabled = colliderActive;
    }
}
