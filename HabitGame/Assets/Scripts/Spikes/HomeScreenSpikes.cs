using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class HomeScreenSpikes : MonoBehaviour
{
    [SerializeField] private SpikeRow[] _spikeRows;
    [SerializeField] private int _spikesInRow = 16;
    [SerializeField] private float _upTime;
    [SerializeField] private float _nextTime;
    [SerializeField] private float _repeatTime;
    [SerializeField] private int _openSpace;

    private void Start()
    {
        for (int i = 0; i < _spikeRows.Length; i++)
        {
            _spikeRows[i].GetSpikes();
        }

        Spike(0);
    }

    private void Spike(int index)
    {
        int2 openSpace = new();
        int maxStartIndex = _spikesInRow - _openSpace;
        openSpace.x = UnityEngine.Random.Range(0, maxStartIndex);
        openSpace.y = openSpace.x + _openSpace;

        StartCoroutine(WaitSpikeNext(openSpace, index));
    }

    private IEnumerator WaitSpikeNext(int2 openSpace, int startRow)
    {
        for (int i = startRow; i < _spikeRows.Length; i++)
        {
            yield return new WaitForSeconds(_nextTime);
            _spikeRows[i].SetSpikeSection(openSpace, _upTime);

        }
        yield return new WaitForSeconds(_repeatTime);
        Spike(0);
    }
}
