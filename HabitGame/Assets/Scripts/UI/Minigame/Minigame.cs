using DTT.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The minigame this script takes care of everything in the minigame
/// </summary>

public class Minigame : MonoBehaviour
{
    [SerializeField] private MinigamePopup _minigamePopup;

    [Header("Spinning")]
    [SerializeField] private Image _spinningImage;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _landingImage;
    [SerializeField] private float _spinningSpeed;
    [SerializeField] private float _noTapTime;
    [Header("Bar")]
    [SerializeField] private RectTransform _barRectTransform;
    [SerializeField] private Point _barPointPrefab;
    [SerializeField] private int _barPoints;

    private Point[] _points;
    private int _currentPoints;
    private RectTransform _rectSpinningImage;
    private bool _isPlaying = false;
    private float _waitTime;

    private void Start()
    {
        _rectSpinningImage = _spinningImage.GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_isPlaying)
        {
            return;
        }

        //Rotate the spinning image
        _rectSpinningImage.Rotate(0, 0, _spinningSpeed * Time.deltaTime);

        if (_waitTime > 0f)
        {
            _waitTime = Mathf.Max(0f, _waitTime - Time.deltaTime);
        }
    }

    public void StartGame()
    {
        gameObject.SetActive(true);

        #region SetPoints
        if (!_points.IsNullOrEmpty())
        {
            for (int i = _points.Length - 1; i >= 0; i--)
            {
                Destroy(_points[i].gameObject);
            }
        }
        _points = new Point[_barPoints];

        for (int i = 0; i < _points.Length; i++)
        {
            Point newPoint = Instantiate(_barPointPrefab, _barRectTransform);
            _points[i] = newPoint;
        }
        #endregion
        _currentPoints = -1;
        _isPlaying = true;
    }

    public void Tap()
    {
        if (!_isPlaying || _waitTime > 0)
        {
            return;
        }

        bool hit = IsAtArrow();
        _waitTime = _noTapTime;
        Point(hit);
    }

    //If hit is true the player gets an extra point 
    //Might add a way you can lose point is you miss 
    private void Point(bool hit)
    {
        if (!hit || _points.IsNullOrEmpty())
        {
            return;
        }

        _currentPoints++;
        if (_currentPoints < _points.Length - 1) //Not completed
        {
            _points[_currentPoints].SetFill(true);
        }
        else 
        {
            //Todo: Show they succeeded somehow then close popup
            //Todo: give player the reward
            _minigamePopup.ShowPopup(false, true);
        }
    }

    private bool IsAtArrow()
    {
        //Check if the spinning image is at the right rotation to be hit
        //The spinning Image has % size of the slider value. 
        float size = _slider.value; //Now 0.25 so 25% of the whole circle
        //A full circle is 360 degrees
        float degrees = 360 * size;
        float eulerAngleZ = _rectSpinningImage.rotation.eulerAngles.z;
        return MathHelper.IsBetween(eulerAngleZ, 0f, degrees);
    }
}
