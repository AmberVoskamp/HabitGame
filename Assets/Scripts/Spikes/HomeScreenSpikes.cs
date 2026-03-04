using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class HomeScreenSpikes : MonoBehaviour
{
    [SerializeField] private SpikeRow[] m_spikeRows;
    [SerializeField] private int m_spikesInRow = 16;
    [SerializeField] private float m_upTime;
    [SerializeField] private float m_nextTime;
    [SerializeField] private float m_repeatTime;
    [SerializeField] private int m_openSpace;

    private void Start()
    {
        for (int i = 0; i < m_spikeRows.Length; i++)
        {
            m_spikeRows[i].SpawnSpikes(m_spikesInRow);
        }

        Spike(0);
    }

    private void Spike(int index)
    {
        int2 openSpace = new int2();
        int maxStartIndex = m_spikesInRow - m_openSpace;
        openSpace.x = UnityEngine.Random.Range(0, maxStartIndex);
        openSpace.y = openSpace.x + m_openSpace;

        StartCoroutine(WaitSpikeNext(openSpace, index));
    }

    IEnumerator WaitSpikeNext(int2 openSpace, int startRow)
    {
        for (int i = startRow; i < m_spikeRows.Length; i++)
        {
            yield return new WaitForSeconds(m_nextTime);
            m_spikeRows[i].SetSpikeSection(openSpace, m_upTime);

        }
        yield return new WaitForSeconds(m_repeatTime);
        Spike(0);
    }
}
