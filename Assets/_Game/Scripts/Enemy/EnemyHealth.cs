using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public System.Action OnDeath;
    public static System.Action<GameObject> OnEnemyKilled;
    public System.Action<float> OnSendHealthPercent;
    public System.Action OnGetHit;

    [SerializeField]
    private GameObject m_triggerCollider = null;

    [SerializeField]
    private EnemyState m_enemyState = null;

    [SerializeField]
    private float m_baseHealth = 10f;

    [SerializeField]
    private float m_fullRegeneRate = 3f;

    [SerializeField, Tooltip("Delay without getting hit to start regenerating")]
    private float m_delayBeforeExitCombat = 5f;

    private float m_inCombatTimer;
    private float m_currentHealth;
    private float m_regenerationStep { get => m_baseHealth / m_fullRegeneRate; }
    private bool CanTakeDamage => m_currentHealth > 0;
    private bool m_canRegenerate;
    private bool m_isInCombat;


    private void OnEnable()
    {
        Projectile.OnProjectileHit += OnProjectileHit;
        m_enemyState.OnInitialize += OnInitialize;
    }

    private void OnDisable()
    {
        Projectile.OnProjectileHit -= OnProjectileHit;
        m_enemyState.OnInitialize -= OnInitialize;
    }


    private void Update()
    {
        if(m_canRegenerate && m_currentHealth < m_baseHealth)
        {
            m_currentHealth += m_regenerationStep * Time.deltaTime;
            OnSendHealthPercent?.Invoke(m_currentHealth / m_baseHealth);

            if(m_currentHealth > m_baseHealth)
            {
                m_currentHealth = m_baseHealth;
                m_canRegenerate = false;
            }
        }

        if (m_isInCombat)
        {
            m_inCombatTimer += Time.deltaTime;

            if(m_inCombatTimer > m_delayBeforeExitCombat)
            {
                m_isInCombat = false;
                m_canRegenerate = true;
                m_inCombatTimer = 0f;
            }
        }
    }


    private void OnInitialize()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_canRegenerate = true;
        m_currentHealth = m_baseHealth;
        OnSendHealthPercent?.Invoke(m_currentHealth / m_baseHealth);
    }

    private void OnProjectileHit(GameObject colliderGameobject, Vector3 sourcePosition, float damage)
    {
        if (colliderGameobject != m_triggerCollider)
            return;

        TakeDamage(damage);
    }


    private void TakeDamage(float damage)
    {
        if (CanTakeDamage == false)
            return;

        m_isInCombat = true;
        m_inCombatTimer = 0f;

        m_canRegenerate = false;

        OnGetHit?.Invoke();
        m_currentHealth -= damage;

        OnSendHealthPercent?.Invoke(m_currentHealth / m_baseHealth);

        if (m_currentHealth <= 0)
        {
            OnEnemyKilled?.Invoke(m_triggerCollider);
            OnDeath?.Invoke();
        }
    }



}
