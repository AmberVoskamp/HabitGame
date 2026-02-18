using System.Collections;
using Unity.Mathematics;
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
    [SerializeField] private Vector2 m_timeLeftAfterSpikes;
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

    public void PastSpikes(int currentDificulty, int maxDificulty)
    {
        float currentTime = m_countdown.CurrentTime;
        //Todo we are at the end of the spikes check if we have around enough time 
        //If we have to much time we can up the dificulty
        //If we have to litle time we should make it easier 
        bool updateDificulty = false;
        if (currentTime < m_timeLeftAfterSpikes.x) //To little time left
        {
            updateDificulty = true;
            currentDificulty--;
        }
        else if (currentTime > m_timeLeftAfterSpikes.y) //To much time left
        {
            updateDificulty = true;
            currentDificulty++;
        }

        if (updateDificulty)
        {
            currentDificulty = math.clamp(currentDificulty, 0, maxDificulty);
            if (ConfigManager.Instance != null)
            {
                ConfigManager.Instance.UpdateSpikeDificulty(currentDificulty);
            }
        }
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
