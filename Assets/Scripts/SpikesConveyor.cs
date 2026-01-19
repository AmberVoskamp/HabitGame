using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesConveyor : MonoBehaviour
{
    [SerializeField] private SpikeRow m_spikeRowPrefab;

    [Header("Settings")]
    [SerializeField] private float m_spawnEverySeconds;
    [SerializeField] private float m_spikeMoveY;
    [SerializeField] private float m_spikeMoveTime;

    private List<SpikeRow> _spikeRowList = new List<SpikeRow>();
    private List<SpikeRow> _disabledSpikeRows = new List<SpikeRow>();

    private void Start()
    {
        SpawnSpikeRow();
    }

    private void SpawnSpikeRow()
    {
        SpikeRow spikeRow = null;
        if (_disabledSpikeRows.Count == 0)
        {
            spikeRow = Instantiate(m_spikeRowPrefab, transform);
            _spikeRowList.Add(spikeRow); //Check if you need this!!!
        }
        else
        {
            spikeRow = _disabledSpikeRows[0];
            _disabledSpikeRows.RemoveAt(0);
            spikeRow.transform.localPosition = Vector3.zero;
            spikeRow.gameObject.SetActive(true);
        }

        if (spikeRow == null)
        {
            Debug.LogWarning("[SpikesConveyor.cs] Something went wrong with spawning the spike row");
            return;
        }

        spikeRow.SetSpikeSection();

        #region MoveSpikeRow
        var test = spikeRow.transform.DOMoveY(m_spikeMoveY, m_spikeMoveTime)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // This code runs ONLY when the movement finishes
                _disabledSpikeRows.Add(spikeRow);
                spikeRow.gameObject.SetActive(false);
            });
        #endregion

        StartCoroutine(WaitSpawnSpikeRow());
    }

    IEnumerator WaitSpawnSpikeRow()
    {
        yield return new WaitForSeconds(m_spawnEverySeconds);
        SpawnSpikeRow();
    }
}
