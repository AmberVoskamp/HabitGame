using UnityEngine;

public class SpikeRow : MonoBehaviour
{
    [SerializeField] private SpikeSection[] m_spikeSections;

    public void SetSpikeSection()
    {
        int randomSpikeIndex = Random.Range(0, m_spikeSections.Length);

        for (int i = 0; i < m_spikeSections.Length; i++)
        {
            SpikeSection spikeSection = m_spikeSections[i];
            spikeSection.Spikes(i != randomSpikeIndex);
        }
    }

}
