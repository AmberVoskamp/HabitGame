using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The players movement script takes the input and sets them to velocity
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed = 5f;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rigidbody.linearVelocity = _moveInput * m_movementSpeed;
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }
}
