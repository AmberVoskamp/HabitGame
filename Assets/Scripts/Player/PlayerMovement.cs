using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed = 5f;

    private Rigidbody2D m_rigidbody;
    private Vector2 m_moveInput;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        m_rigidbody.linearVelocity = m_moveInput * m_movementSpeed;
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        m_moveInput = callbackContext.ReadValue<Vector2>();
    }
}
