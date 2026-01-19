using DTT.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
    [SerializeField] private MinigamePopup m_minigamePopup;

    [Header("Spinning")]
    [SerializeField] private Image m_spinningImage;
    [SerializeField] private Slider m_slider;
    [SerializeField] private Image m_landingImage;
    [SerializeField] private float m_spinningSpeed;
    [SerializeField] private float m_noTapTime;
    [Header("Bar")]
    [SerializeField] private RectTransform m_barRectTransform;
    [SerializeField] private Point m_barPointPrefab;
    [SerializeField] int m_barPoints;

    private Point[] _points;
    private int _currentPoints;
    private RectTransform _rectSpinningImage;
    private bool _isPlaying = false;
    private float _waitTime;

    private void Start()
    {
        _rectSpinningImage = m_spinningImage.GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_isPlaying)
        {
            return;
        }

        //Rotate the spinning image
        _rectSpinningImage.Rotate(0, 0, m_spinningSpeed * Time.deltaTime);

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
        _points = new Point[m_barPoints];

        for (int i = 0; i < _points.Length; i++)
        {
            Point newPoint = Instantiate(m_barPointPrefab, m_barRectTransform);
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
        _waitTime = m_noTapTime;
        Point(hit);
    }

    //If hit is true the player gets an extra point 
    //Might add a way you can lose point is you miss 
    private void Point(bool hit)
    {
        Debug.Log($"[Minigame] {hit}");
        if (!hit || _points.IsNullOrEmpty())
        {
            return;
        }

        _currentPoints++;
        if (_currentPoints < _points.Length - 1) //Not completed
        {
            _points[_currentPoints].SetFill(true);
        }
        else //completed end minigame
        {
            //Todo: Show they succeeded somehow then close popup
            //Todo: give player the reward
            m_minigamePopup.ShowPopup(false);
        }
    }

    private bool IsAtArrow()
    {
        //Check if the spinning image is at the right rotation to be hit
        //The spinning Image has % size of the slider value. 
        float size = m_slider.value; //Now 0.25 so 25% of the whole circle
        //A full circle is 360 degrees
        float degrees = 360 * size;
        float eulerAngleZ = _rectSpinningImage.rotation.eulerAngles.z;
        return MathHelper.IsBetween(eulerAngleZ, 0f, degrees);
    }
}
