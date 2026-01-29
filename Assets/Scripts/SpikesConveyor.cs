using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpikesConveyor : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_player;
    [SerializeField] private SpikeRow m_spikeRowPrefab;

    [Header("Settings")]
    [SerializeField] private float m_spawnEverySeconds;
    [SerializeField] private float m_spikeMoveY;
    [SerializeField] private float m_spikeMoveTime;
    [SerializeField] private ConveyorSettings[] m_dificultySettings;

    private List<SpikeRow> _spikeRowList = new List<SpikeRow>();
    private List<SpikeRow> _disabledSpikeRows = new List<SpikeRow>();
    private Coroutine _spikeCoroutine;
    private bool _isActive = false;
    private int _currentDificulty;

    [Serializable]
    public struct ConveyorSettings
    {
        public float spawnEverySeconds;
        public float spikeMoveTime;
    }

    private void Start()
    {
        _isActive = true;
        if (ConfigManager.Instance != null)
        {
            _currentDificulty = ConfigManager.Instance.SpikeDificulty;
        }

        SpawnSpikeRow();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!_isActive)
        {
            return;
        }

        if (col.gameObject == m_player.gameObject)
        {
            Debug.Log("End spikesConveyor");
            _isActive = false;
            if (_spikeCoroutine != null)
            {
                StopCoroutine(_spikeCoroutine);
            }

            PlayerHealth.Instance.PastSpikes(_currentDificulty, m_dificultySettings.Length -1);
        }
    }


    private void SpawnSpikeRow()
    {
        if (!_isActive)
        {
            return;
        }

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

        if (!_isActive)
        {
            return;
        }

        _spikeCoroutine = StartCoroutine(WaitSpawnSpikeRow());
    }

    IEnumerator WaitSpawnSpikeRow()
    {
        yield return new WaitForSeconds(m_spawnEverySeconds);
        SpawnSpikeRow();
        _spikeCoroutine = null;
    }
}
