using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShield : MonoBehaviour
{
    public static System.Action<GameObject> OnShieldBroken;

    [SerializeField]
    private EnemyState m_enemyState = null;

    [SerializeField]
    private Rigidbody m_body = null;

    [SerializeField]
    private GameObject m_visualObject = null;

    [SerializeField]
    private Collider m_collider = null;

    [SerializeField]
    private float m_shieldHealth = 1;

    [SerializeField]
    private Vector3 m_positionOffsetFromTarget = Vector3.zero;


    private float m_currentHealth;


    private void OnEnable()
    {
        m_enemyState.OnInitialize += OnInitialize;
        m_enemyState.OnDisableEnemy+= OnDisableEnemy;
    }

    private void OnDisable()
    {
        m_enemyState.OnInitialize -= OnInitialize;
        m_enemyState.OnDisableEnemy -= OnDisableEnemy;
    }


    private void Update()
    {
        if (m_body != null)
            m_body.transform.localPosition = m_positionOffsetFromTarget;
    }


    private void OnInitialize()
    {
        m_currentHealth = m_shieldHealth;
        m_visualObject.SetActive(true);
        m_collider.enabled = true;
        Projectile.OnProjectileHit += OnProjectileHit;
    }

    private void OnDisableEnemy()
    {
        m_visualObject.SetActive(false);
        m_collider.enabled = false;
        OnShieldBroken?.Invoke(m_body.gameObject);
        Projectile.OnProjectileHit -= OnProjectileHit;
    }

    private void OnProjectileHit(GameObject colliderGameobject, Vector3 sourcePosition, float damage)
    {
        if (colliderGameobject != m_body.gameObject)
            return;

        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        m_currentHealth -= damage;

        if (m_currentHealth < 0)
        {
            OnShieldBroken?.Invoke(m_body.gameObject);
            m_visualObject.SetActive(false);
            m_collider.enabled = false;
            Projectile.OnProjectileHit -= OnProjectileHit;
        }
    }

}
