using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is for showing if you got a point in the minigame
/// </summary>

public class Point : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    private void Start()
    {
        SetFill(false);
    }

    public void SetFill(bool active)
    {
        _fillImage.gameObject.SetActive(active);
    }
}
