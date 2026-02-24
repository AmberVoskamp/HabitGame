using DTT.Utils.Extensions;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SpikeRow : MonoBehaviour
{
    [SerializeField] private Spikes m_spikePrefab;
    [SerializeField] private float m_spaceBetweenSpikes;

    private Spikes[] m_spikeArray;

    public void SetSpikeSection(int2 openSpace, float upTime)
    {
        for (int i = 0; i < m_spikeArray.Length; i++)
        {
            if (i >= openSpace.x && i <= openSpace.y)
            {
                continue;
            }

            m_spikeArray[i].SpikeUp(true);
        }

        StartCoroutine(SpikeUp(upTime));
    }

    private void SpikesDown()
    {
        for (int i = 0; i < m_spikeArray.Length; i++)
        {
            m_spikeArray[i].SpikeUp(false);
        }
    }

    public void SpawnSpikes(int size)
    {
        if (!m_spikeArray.IsNullOrEmpty())
        {
            return;
        }

        #region spawn spikes
        m_spikeArray = new Spikes[size];
        float oldSpikePlacement = -m_spaceBetweenSpikes;
        for (int i = 0; i < size; i++)
        {
            Spikes spike = Instantiate(m_spikePrefab, transform);
            float newSpikePlacement = oldSpikePlacement + m_spaceBetweenSpikes;
            spike.transform.localPosition = new Vector3(newSpikePlacement, 0, 0);
            oldSpikePlacement = newSpikePlacement;
            m_spikeArray[i] = spike;
        }
        #endregion
    }

    IEnumerator SpikeUp(float upTime)
    {
        yield return new WaitForSeconds(upTime);
        SpikesDown();
    }
}
