using System.Collections;
using UnityEngine;

/*
 * This player health script will take care of taking damage
 * The health of the player isn't hearts but the time they have left
 */
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [SerializeField] private CountDown m_countdown;
    [SerializeField] private float m_timer = 60f;
    [SerializeField, Tooltip("The seconds that will be removed")] private float m_damageSeconds;
    [SerializeField] private float m_damageTime;
    [SerializeField] private Color m_takeDamageColor;
    [SerializeField] private SpriteRenderer m_playerSprite;

    private Camera m_mainCamera;
    private Color m_basicPlayerColor;
    private bool m_isTakingDamage;

    private void Awake()
    {
        Instance = this;
        m_mainCamera = Camera.main;
        m_basicPlayerColor = m_playerSprite.color;
        gameObject.SetActive(false);
    }

    public void StartGame()
    {
        m_countdown.gameObject.SetActive(true);
        m_countdown.StartCountdown(m_timer);
    }

    public void TakeDamage(int damage)
    {
        if (m_isTakingDamage)
        {
            return;
        }

        Vector3 screenPosition = m_mainCamera.WorldToScreenPoint(transform.position);
        m_countdown.LoseTime(damage * m_damageSeconds, screenPosition);
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
