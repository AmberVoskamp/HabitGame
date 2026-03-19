using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The players movement script takes the input and sets them to velocity
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed = 5f;

    private MinigamePopup _popup;
    private UIManager _uiManager;
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
        _uiManager = UIManager.Instance;
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
        if (_uiManager == null)
        {
            return;
        }

        if (_isInMinigameRange)
        {
            _uiManager.ShowMinigame(true);
        }
    }

    public void MinigameTap()
    {
        if (_uiManager == null)
        {
            return;
        }

        _uiManager.MinigameTap();
    }

    public void TutorialClick()
    {
        if (_uiManager == null)
        {
            return;
        }

        _uiManager.TutorialClick();
    }
}
