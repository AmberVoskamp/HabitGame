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

    public Transform Target
    {
        set { _target = value; }
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        Vector3 targetPosition = _target.position + m_offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}