using DTT.Utils.Extensions;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Spikerow are the rows of spikes and here comes in what the open space is 
/// and set the spikes up or down
/// </summary>

public class SpikeRow : MonoBehaviour
{
    [SerializeField] private Spikes _spikePrefab;
    [SerializeField] private float _spaceBetweenSpikes;

    private Spikes[] _spikeArray;

    public void SetSpikeSection(int2 openSpace, float upTime)
    {
        for (int i = 0; i < _spikeArray.Length; i++)
        {
            if (i >= openSpace.x && i <= openSpace.y)
            {
                continue;
            }

            _spikeArray[i].SpikeUp(true);
        }

        StartCoroutine(SpikeUp(upTime));
    }

    private void SpikesDown()
    {
        for (int i = 0; i < _spikeArray.Length; i++)
        {
            _spikeArray[i].SpikeUp(false);
        }
    }

    public void SpawnSpikes(int size)
    {
        if (!_spikeArray.IsNullOrEmpty())
        {
            return;
        }

        if (transform.childCount > 0)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = transform.GetChild(i).gameObject;
                DestroyImmediate(child);
            }
        }

        #region spawn spikes
        _spikeArray = new Spikes[size];
        float oldSpikePlacement = -_spaceBetweenSpikes;
        for (int i = 0; i < size; i++)
        {
            Spikes spike = Instantiate(_spikePrefab, transform);
            float newSpikePlacement = oldSpikePlacement + _spaceBetweenSpikes;
            spike.transform.localPosition = new Vector3(newSpikePlacement, 0, 0);
            oldSpikePlacement = newSpikePlacement;
            _spikeArray[i] = spike;
        }
        #endregion
    }

    public void GetSpikes()
    {
        if (!_spikeArray.IsNullOrEmpty())
        {
            return;
        }

        _spikeArray = GetComponentsInChildren<Spikes>(); 
    }

    private IEnumerator SpikeUp(float upTime)
    {
        yield return new WaitForSeconds(upTime);
        SpikesDown();
    }
}
