using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using static SpikesConveyor;

/// <summary>
/// The spike converyor manages all the spikes
/// It will spawn all the spikes in and when you trigger the box collider the spikes will stop
/// This will also take care of where the spikes in a row will be up and down
/// </summary>

[RequireComponent(typeof(BoxCollider2D))]
public class SpikesConveyor : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private SpikeRow m_spikeRowPrefab;

    [Header("Settings")]
    [SerializeField] private int m_spikesInRow = 16;
    [SerializeField] private int m_spawnRows;
    [SerializeField] private float m_spawnRowsYDistance;

    [Space]
    [SerializeField] private int m_openSpace = 5;
    [SerializeField] private int m_spawnEveryRows;
    [SerializeField] private float m_spikeMoveNextTime;
    [SerializeField] private float m_spikesUpSeconds;
    [SerializeField] private ConveyorSettings[] m_dificultySettings;

    private SpikeRow[] _spikeRows;
    private Coroutine _spikeCoroutine;
    private bool _isActive = false;
    private int _currentDificulty;
    private ConveyorSettings _conveyorSettings;

    [Serializable]
    public struct ConveyorSettings
    {
        public int openSpace;
        public int spawnEveryRows;
    }

    private void Start()
    {
        _isActive = true;
        if (ConfigManager.Instance != null)
        {
            _currentDificulty = ConfigManager.Instance.SpikeDificulty;
        }
        _conveyorSettings = m_dificultySettings[_currentDificulty];

        Phase phase = GetComponentInParent<Phase>();
        m_gameManager = phase.GameManager;

        SpawnSpikeRows();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!_isActive || !col.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            return;
        }

        _isActive = false;
        if (_spikeCoroutine != null)
        {
            StopCoroutine(_spikeCoroutine);
        }

        m_gameManager.SpikeSectionDone(playerHealth.CurrentHealth, m_dificultySettings.Length - 1);
    }

    private void SpawnSpikeRows()
    {
        _spikeRows = new SpikeRow[m_spawnRows];
        float yOffset = 0;
        for (int i = 0; i < m_spawnRows; i++)
        {
            var spikeRow = Instantiate(m_spikeRowPrefab, transform);
            spikeRow.transform.localPosition = new Vector3(0, yOffset, 0);
            yOffset += m_spawnRowsYDistance;
            spikeRow.SpawnSpikes(m_spikesInRow);
            _spikeRows[i] = spikeRow;
        }

        StartSpike();
    }

    private void StartSpike()
    {
        int index = 0;
        while (index < _spikeRows.Length)
        {
            Spike(index);
            index += _conveyorSettings.spawnEveryRows;
        }
    }

    private void Spike(int index)
    {
        if (!_isActive)
        {
            return;
        }

        int2 openSpace = new int2();
        int maxStartIndex = m_spikesInRow - _conveyorSettings.openSpace;
        openSpace.x = UnityEngine.Random.Range(0, maxStartIndex);
        openSpace.y = openSpace.x + _conveyorSettings.openSpace;

        StartCoroutine(WaitSpikeNext(openSpace, index));
    }

    IEnumerator WaitSpikeNext(int2 openSpace, int startRow)
    {
        for (int i = startRow; i < _spikeRows.Length; i++)
        {
            yield return new WaitForSeconds(m_spikeMoveNextTime);
            _spikeRows[i].SetSpikeSection(openSpace, m_spikesUpSeconds);

            if (i == _conveyorSettings.spawnEveryRows && startRow == 0)
            {
                Spike(0);
            }
        }
    }
}
