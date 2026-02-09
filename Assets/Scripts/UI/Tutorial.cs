using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Button m_button;

    private void Start()
    {
        gameObject.SetActive(false);
        m_button.onClick.AddListener(ContinueGame);
    }

    public void PauseGame()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void ContinueGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    } 
}
