using System.Collections;
using UnityEngine;

public class SpikeSection : MonoBehaviour
{
    [SerializeField] private bool m_goesWithTimer = false;

    [Header("TimerSettings")]
    [SerializeField] private float m_startAfterSeconds;
    [SerializeField] private float m_spikesUpTime;
    [SerializeField] private float m_spikesDownTime;

    private Spikes[] m_spikes;

    void Awake()
    {
        m_spikes = GetComponentsInChildren<Spikes>();
        if (m_goesWithTimer)
        {
            Spikes(false);
            //StartCoroutine(StartAfter());
        }
        else
        {
            Spikes(true);
        }
    }

    public void Spikes(bool up)
    {
        foreach (var spike in m_spikes)
        {
            spike.SpikeUp(up);
        }
    }

    IEnumerator StartAfter()
    {
        yield return new WaitForSeconds(m_startAfterSeconds);
       /* StartCoroutine(SpikesRoutine());*/
    }

 /*   IEnumerator SpikesRoutine()
    {
        //spikes up
        Spikes(true);
        yield return new WaitForSeconds(m_spikesUpTime);
        Spikes(false);
        //spikes down
        yield return new WaitForSeconds(m_spikesDownTime);
        //repeat
        StartCoroutine(SpikesRoutine());
    }*/
}
