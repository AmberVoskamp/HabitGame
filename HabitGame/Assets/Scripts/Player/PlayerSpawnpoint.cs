using UnityEngine;

public class PlayerSpawnpoint : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerPrefab;

    private CameraFollow _cameraFollow;

    private void Awake()
    {
        _cameraFollow = Camera.main.gameObject.GetComponent<CameraFollow>();
    }

    public PlayerHealth SpawnPlayer()
    {
        PlayerHealth player = Instantiate(_playerPrefab, transform.position, Quaternion.identity);

        if (_cameraFollow != null)
        {
            _cameraFollow.SetCameraPosToTarget(player.transform);
        }

        return player;
    }
}
