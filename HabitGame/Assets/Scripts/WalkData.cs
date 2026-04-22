using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WalkData : MonoBehaviour
{
    [SerializeField] private float _distanceBetweenData;

    private PlayerHealth _player;
    private bool _recording;
    private List<Data> _walkData;
    private Vector2 _offset;

    [Serializable]
    public struct Data
    {
        public float TimeLeft;
        public Vector2 Position;
    }

    public void Record(bool record, PlayerHealth player = null)
    {
        _recording = record;

        if (record)
        {
            _walkData = new List<Data>();
            _player = player;
        }
        else
        {
            //Send data to config
            ConfigManager.Instance?.SafeWalkData(_walkData.ToArray());
        }
    }

    private void FixedUpdate()
    {
        if (!_recording)
        {
            return;
        }

        Vector2 newPosition = new()
        {
            x = _player.transform.position.x - _offset.x,
            y = _player.transform.position.y - _offset.y
        };

        if (_walkData.Count == 0)
        {
            _offset = newPosition;
            AddWalkData(Vector2.zero);
            return;
        }

        int lastIndex = _walkData.Count - 1;
        Vector2 lastPosition = _walkData[lastIndex].Position;

        if (math.distance(newPosition, lastPosition) > _distanceBetweenData)
        {
            AddWalkData(newPosition);
        }
    }

    private void AddWalkData(Vector2 newPosition)
    {
        Data data = new()
        {
            TimeLeft = _player.GetCurrentHealth,
            Position = newPosition
        };

        _walkData.Add(data);
    }
}
