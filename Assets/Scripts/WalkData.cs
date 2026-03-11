using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WalkData : MonoBehaviour
{
    [SerializeField] private float m_distanceBetweenData;

    private PlayerHealth _player;
    private bool _recording;
    private List<Data> _walkData;

    [Serializable]
    struct Data
    {
        public float timeLeft;
        public Vector2 position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_recording && col.gameObject.TryGetComponent<PlayerHealth>(out _player))
        {
            _walkData = new List<Data>();
            _recording = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_recording)
        {
            return;
        }

        Vector2 newPosition = new Vector2()
        {
            x = _player.transform.position.x,
            y = _player.transform.position.y
        };

        if (_walkData.Count == 0)
        {
            AddWalkData(newPosition);
            return;
        }

        int lastIndex = _walkData.Count - 1;
        Vector2 lastPosition = _walkData[lastIndex].position;

        if (math.distance(newPosition, lastPosition) > m_distanceBetweenData)
        {
            AddWalkData(newPosition);
        }
    }

    public void StopRecording()
    {
        _recording = false;
    }

    private void AddWalkData(Vector2 newPosition)
    {
        Data data = new Data();
        data.timeLeft = _player.CurrentHealth;
        data.position = new Vector2()
        {
            x = _player.transform.position.x,
            y = _player.transform.position.y
        };

        _walkData.Add(data);
    }
}
