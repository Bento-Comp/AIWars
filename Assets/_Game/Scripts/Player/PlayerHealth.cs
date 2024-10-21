using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static System.Action OnPlayerInvincible;
    public static System.Action OnPlayerNotInvincible;
    public static System.Action OnPlayerTakeDamage;
    public static System.Action<int> OnSendCurrentHealth;
    public static System.Action OnPlayerDeath;


    [SerializeField]
    private ColliderObjectDetector m_bodyEnemyDetector = null;

    [SerializeField]
    private float m_invincibilityTime = 2f;

    [SerializeField]
    private float m_regenerationSpeed = 0.05f;

    [SerializeField]
    private int m_maxHealth = 3;



    private Coroutine m_invincibilityCoroutine;
    private float m_regenerateHealthTimer;
    private int m_currentHealth;
    private bool m_isInvincible;
    public bool IsAlive { get => m_currentHealth > 0; }

    private void OnEnable()
    {
        m_bodyEnemyDetector.OnObjectDetected += OnObjectDetected;
        PlayerRespawn.OnPlayerRespawn += OnPlayerRespawn;
    }

    private void OnDisable()
    {
        m_bodyEnemyDetector.OnObjectDetected -= OnObjectDetected;
        PlayerRespawn.OnPlayerRespawn -= OnPlayerRespawn;
    }


    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        RegenerateHealth();
    }

    private void OnPlayerRespawn()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_isInvincible = false;
        m_currentHealth = m_maxHealth;
        SendCurrentHealth();

        m_regenerateHealthTimer = 0f;
    }

    private void RegenerateHealth()
    {
        if (IsAlive == false)
            return;

        if (m_currentHealth >= m_maxHealth)
            return;

        m_regenerateHealthTimer += Time.deltaTime;

        if (m_regenerateHealthTimer > 1 / m_regenerationSpeed)
        {
            m_regenerateHealthTimer = 0f;
            m_currentHealth++;
            SendCurrentHealth();
        }
    }

    private void OnObjectDetected(GameObject enemyColliderObject)
    {
        TakeDamage();
    }

    private void TakeDamage()
    {
        if (m_isInvincible)
            return;

        if (m_invincibilityCoroutine != null)
            StopCoroutine(m_invincibilityCoroutine);


        m_currentHealth--;
        OnPlayerTakeDamage?.Invoke();

        SendCurrentHealth();

        if (m_currentHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
        else
        {
            m_invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        OnPlayerInvincible?.Invoke();
        m_isInvincible = true;
        yield return new WaitForSeconds(m_invincibilityTime);
        m_isInvincible = false;
        OnPlayerNotInvincible?.Invoke();
    }

    private void SendCurrentHealth()
    {
        OnSendCurrentHealth?.Invoke(m_currentHealth);
    }
}
