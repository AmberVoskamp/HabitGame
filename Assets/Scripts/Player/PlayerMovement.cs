using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The players movement script takes the input and sets them to velocity
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed = 5f;

    private UIManager _uiManager;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;
    private bool _isInMinigameRange;

    public bool IsInMinigameRange
    {
        set { _isInMinigameRange = value; }
    }

    #region Unity methods

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _uiManager = UIManager.Instance;
    }

    void FixedUpdate()
    {
        _rigidbody.linearVelocity = _moveInput * m_movementSpeed;
    }

    #endregion

    public void Move(InputAction.CallbackContext callbackContext)
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }

    //Todo Move player to next entrance and make them move X amount forward
    public void Entrance(Vector3 newPos)
    {
        transform.position = newPos;
    }

    #region Inputs

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

    #endregion
}
