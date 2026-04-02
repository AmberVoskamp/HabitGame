using System.Collections;
using UnityEngine;

/// <summary>
/// These are the individual spikes which deprives from DamageObjects
/// If the player hits the spikes when they are up they take damage
/// </summary>

public class Spikes : DamageObject
{
    [SerializeField] private Animator m_animator;

    private bool _spikes;

    void Start()
    {
        m_boxCollider2D.enabled = false;
    }

    public void SpikeUp(bool up)
    {
        if (_spikes == up)
        {
            return;
        }

        _spikes = up;
        string animation = up ? "Open" : "Close";
        m_animator.SetTrigger(animation);

        if (up)
        {
            float animationTime = m_animator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(WaitForAnimation(animationTime, up));
            return;
        }

        m_boxCollider2D.enabled = up;
    }

    IEnumerator WaitForAnimation(float animationTime, bool colliderActive)
    {
        yield return new WaitForSeconds(animationTime);
        m_boxCollider2D.enabled = colliderActive;
    }
}
