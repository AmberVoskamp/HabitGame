using UnityEngine;

/// <summary>
/// The CameraFollow script is for so the camera will follow the target around the scene
/// </summary>

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 m_offset = new Vector3(0f, -10f, -10f);
    
    private Transform _target;

    private float _smoothTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;

    public void SetCameraPosToTarget(Transform newTarget)
    {
        _target = null;
        Vector3 targetPosition = newTarget.position + m_offset;
        transform.position = targetPosition;

        _target = newTarget;
    }

    private void Update()
    {
        if (_target == null)
        {
            Debug.Log("camera target not set");
            return;
        }

        Vector3 targetPosition = _target.position + m_offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}