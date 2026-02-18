using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private float m_fadeTime;
    private Image _image;

    void Start()
    {
        gameObject.SetActive(true);
        _image = GetComponent<Image>();
        Color color = _image.color;
        color.a = 0f;
        _image.color= color;
    }

    public void Fade()
    {
        _image.DOFade(1f, m_fadeTime)
            .OnComplete(() =>
            {
                SceneSwitchManager.Instance.SwitchScene(Scenes.HomeScene);
            });
    }
}
