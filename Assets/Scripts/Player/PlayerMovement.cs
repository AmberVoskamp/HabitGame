using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The players movement script takes the input and sets them to velocity
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed = 5f;

    private MinigamePopup _popup;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;
    private bool _isInMinigameRange;

    public bool IsInMinigameRange
    {
        set { _isInMinigameRange = value; }
    }

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

    public void OpenMinigamePopup()
    {
        if (_popup == null)
        {
            _popup = MinigamePopup.Instance;
            if (_popup == null)
            {
                return;
            }
        }

        if (_isInMinigameRange)
        {
            _popup.ShowPopup(true);
        }
    }

    public void MinigameTap()
    {
        if (_popup == null)
        {
            return;
        }

        _popup.Minigame.Tap();
    }
}
