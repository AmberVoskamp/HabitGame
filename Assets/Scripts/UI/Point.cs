using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    [SerializeField] private Image m_fillImage;

    private void Start()
    {
        SetFill(false);
    }

    public void SetFill(bool active)
    {
        m_fillImage.gameObject.SetActive(active);
    }
}
