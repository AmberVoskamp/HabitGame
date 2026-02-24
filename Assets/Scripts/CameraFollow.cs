using UnityEngine;

/// <summary>
/// The CameraFollow script is for so the camera will follow the target around the scene
/// </summary>

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 m_offset = new Vector3(0f, -10f, -10f);
    [SerializeField] private Transform m_target;

    private float _smoothTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        if (m_target == null)
        {
            return;
        }

        Vector3 targetPosition = m_target.position + m_offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}