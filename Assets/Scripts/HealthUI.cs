using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image m_heartPrefab;

    private List<Image> m_hearts;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetHealth(int hearts)
    {
        m_hearts = new List<Image>();

        for (int i = 0; i < hearts; i++)
        {
            Image heart = Instantiate(m_heartPrefab, gameObject.transform);
            m_hearts.Add(heart);
        }
    }

    public void UpdateHealth(int hearts)
    {
        if (m_hearts.Count == 0)
        {
            return;
        }

        int loseHearts = m_hearts.Count - hearts;

        for (int i = 0; i < loseHearts; i++)
        {
           m_hearts[0].gameObject.SetActive(false);
           m_hearts.RemoveAt(0);
        }
    }
}
