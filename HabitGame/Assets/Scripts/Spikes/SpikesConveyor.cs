using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// The spike converyor manages all the spikes
/// It will spawn all the spikes in and when you trigger the box collider the spikes will stop
/// This will also take care of where the spikes in a row will be up and down
/// </summary>

[RequireComponent(typeof(BoxCollider2D))]
public class SpikesConveyor : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SpikeRow _spikeRowPrefab;

    [Header("Settings")]
    [SerializeField] private int _spikesInRow = 16;
    [SerializeField] private int _spawnRows;
    [SerializeField] private float _spawnRowsYDistance;

    [Space]
    [SerializeField] private int _openSpace = 5;
    [SerializeField] private int _spawnEveryRows;
    [SerializeField] private float _spikeMoveNextTime;
    [SerializeField] private float _spikesUpSeconds;
    [SerializeField] private ConveyorSettings[] _dificultySettings;

    private SpikeRow[] _spikeRows;
    private readonly Coroutine _spikeCoroutine;
    private bool _isActive = false;
    private int _currentDificulty;
    private ConveyorSettings _conveyorSettings;

    [Serializable]
    public struct ConveyorSettings
    {
        public int OpenSpace;
        public int SpawnEveryRows;
    }

    private void Start()
    {
        _isActive = true;
        if (ConfigManager.Instance != null)
        {
            _currentDificulty = ConfigManager.Instance.SpikeDificulty;
        }
        _conveyorSettings = _dificultySettings[_currentDificulty];

        Phase phase = GetComponentInParent<Phase>();
        _gameManager = phase.GameManager;

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

        _gameManager.SpikeSectionDone(playerHealth.GetCurrentHealth, _dificultySettings.Length - 1);
    }

    private void SpawnSpikeRows()
    {
        _spikeRows = new SpikeRow[_spawnRows];
        float yOffset = 0;
        for (int i = 0; i < _spawnRows; i++)
        {
            SpikeRow spikeRow = Instantiate(_spikeRowPrefab, transform);
            spikeRow.transform.localPosition = new Vector3(0, yOffset, 0);
            yOffset += _spawnRowsYDistance;
            spikeRow.SpawnSpikes(_spikesInRow);
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
            index += _conveyorSettings.SpawnEveryRows;
        }
    }

    private void Spike(int index)
    {
        if (!_isActive)
        {
            return;
        }

        int2 openSpace = new();
        int maxStartIndex = _spikesInRow - _conveyorSettings.OpenSpace;
        openSpace.x = UnityEngine.Random.Range(0, maxStartIndex);
        openSpace.y = openSpace.x + _conveyorSettings.OpenSpace;

        StartCoroutine(WaitSpikeNext(openSpace, index));
    }

    private IEnumerator WaitSpikeNext(int2 openSpace, int startRow)
    {
        for (int i = startRow; i < _spikeRows.Length; i++)
        {
            yield return new WaitForSeconds(_spikeMoveNextTime);
            _spikeRows[i].SetSpikeSection(openSpace, _spikesUpSeconds);

            if (i == _conveyorSettings.SpawnEveryRows && startRow == 0)
            {
                Spike(0);
            }
        }
    }
}
