using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFxController : MonoBehaviour
{
    [SerializeField]
    private EnemyState m_enemyState = null;

    [SerializeField]
    private EnemyHealth m_enemyHealth = null;

    [SerializeField]
    private ParticleSystem m_deathFx = null;

    [SerializeField]
    private ParticleSystem m_getHitFx = null;

    [SerializeField]
    private ParticleSystem m_sparkleFx = null;

    private void OnEnable()
    {
        m_enemyState.OnDisableEnemy += OnDisableEnemy;
        m_enemyState.OnInitialize += OnInitialize;
        m_enemyHealth.OnGetHit += OnGetHit;
    }

    private void OnDisable()
    {
        m_enemyState.OnDisableEnemy -= OnDisableEnemy;
        m_enemyState.OnInitialize -= OnInitialize;
        m_enemyHealth.OnGetHit -= OnGetHit;
    }


    private void Start()
    {
        m_deathFx.Stop();
        m_getHitFx.Stop();
    }

    private void OnInitialize()
    {
        if (m_sparkleFx != null)
            m_sparkleFx.Play();
    }

    private void OnDisableEnemy()
    {
        if (m_sparkleFx != null)
            m_sparkleFx.Stop();

        m_deathFx.Play();
    }

    private void OnGetHit()
    {
        m_getHitFx.Play();
    }
}
