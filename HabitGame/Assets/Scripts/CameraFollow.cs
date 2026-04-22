using UnityEngine;

/// <summary>
/// The CameraFollow script is for so the camera will follow the target around the scene
/// </summary>

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = new(0f, -10f, -10f);

    private Transform _target;
    private bool _enabled = false;

    private readonly float _smoothTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;

    public void SetCameraPosToTarget(Transform newTarget)
    {
        _target = newTarget;
        PosToTarget();
    }

    public void PosToTarget()
    {
        _enabled = false;
        Vector3 targetPosition = _target.position + _offset;
        transform.position = targetPosition;
        _enabled = true;
    }

    private void Update()
    {
        if (!_enabled)
        {
            return;
        }

        Vector3 targetPosition = _target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}