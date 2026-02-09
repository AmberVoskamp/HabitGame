using DTT.Utils.Extensions;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpikeRow : MonoBehaviour
{
    [SerializeField] private Spikes m_spikePrefab;
    [SerializeField] private float m_spaceBetweenSpikes;
    [SerializeField] private int m_spikeRowSize;

    [SerializeField] private int m_openSpace = 5;

    private Spikes[] m_spikeArray;
    private int2 m_test;

    public void SetSpikeSection()
    {
        SpawnSpikes();

        #region Reset old spikes
        if (m_test.y != 0)
        {
            for (int i = m_test.x; i < m_test.y + 1; i++)
            {
                m_spikeArray[i].SpikeUp(true);
            }
        }
        #endregion

        #region Get index
        int maxStartIndex = m_spikeRowSize - m_openSpace;
        m_test.x = Random.Range(0, maxStartIndex);
        m_test.y = m_test.x + m_openSpace;

        for (int i = m_test.x; i < m_test.y + 1; i++)
        {
            m_spikeArray[i].SpikeUp(false);
        }
        #endregion
    }

    private void SpawnSpikes()
    {
        if (!m_spikeArray.IsNullOrEmpty())
        {
            return;
        }

        #region spawn spikes
        m_spikeArray = new Spikes[m_spikeRowSize];
        float oldSpikePlacement = -m_spaceBetweenSpikes;
        for (int i = 0; i < m_spikeRowSize; i++)
        {
            Spikes spike = Instantiate(m_spikePrefab, transform);
            float newSpikePlacement = oldSpikePlacement + m_spaceBetweenSpikes;
            spike.transform.localPosition = new Vector3(newSpikePlacement, 0, 0);
            oldSpikePlacement = newSpikePlacement;
            m_spikeArray[i] = spike;
        }
        #endregion
    }
}
