using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The players movement script takes the input and sets them to velocity
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;

    private UIManager _uiManager;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;
    private bool _isInMinigameRange;
    private CameraFollow _cameraFollow;
    private Animator _animator;

    public bool IsInMinigameRange
    {
        set => _isInMinigameRange = value;
    }

    #region Unity methods
    private void Start()
    {
        _cameraFollow = Camera.main.gameObject.GetComponent<CameraFollow>();
        _uiManager = UIManager.Instance;

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _moveInput * _movementSpeed;
    }

    #endregion

    public void Move(InputAction.CallbackContext callbackContext)
    {
        _animator.SetBool("isWalking", true);

        if (callbackContext.canceled)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetFloat("LastInputX", _moveInput.x);
            _animator.SetFloat("LastInputY", _moveInput.y);
        }

        _moveInput = callbackContext.ReadValue<Vector2>();
        _animator.SetFloat("InputX", _moveInput.x);
        _animator.SetFloat("InputY", _moveInput.y);

        Vector3 newRotation = Vector3.zero;
        if (_moveInput.x < 0)
        {
            newRotation.y = 180;
        }
        _animator.transform.rotation = Quaternion.Euler(newRotation);
    }

    //Todo Move player to next entrance and make them move X amount forward
    public void Entrance(Vector3 newPos)
    {
        //Set camera 
        transform.localPosition = newPos;
        _cameraFollow.PosToTarget();
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
