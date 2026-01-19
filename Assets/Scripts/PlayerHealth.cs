using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [SerializeField] private HealthUI m_healthUI;
    [SerializeField] private int m_health;
    [SerializeField] private float m_damageTime;
    [SerializeField] private Color m_takeDamageColor;
    [SerializeField] private SpriteRenderer m_playerSprite;

    private Color m_basicPlayerColor;
    private bool m_isTakingDamage;

    private void Awake()
    {
        Instance = this;
        m_basicPlayerColor = m_playerSprite.color;
        m_healthUI.SetHealth(m_health);
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (m_isTakingDamage)
        {
            return;
        }
        m_health -= damage;
        m_healthUI.UpdateHealth(m_health);
        //todo Update ui
        StartCoroutine(Damage());
    }

    IEnumerator Damage()
    {
        m_isTakingDamage = true;
        m_playerSprite.color = m_takeDamageColor;
        yield return new WaitForSeconds(m_damageTime);
        m_playerSprite.color = m_basicPlayerColor;
        m_isTakingDamage = false;
    }
}
