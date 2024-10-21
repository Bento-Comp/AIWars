using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_healthBarObject = null;

    [SerializeField]
    private EnemyState m_enemyState = null;

    [SerializeField]
    private EnemyHealth m_enemyHealth = null;

    [SerializeField]
    private Slider m_healthBarSlider = null;

    [SerializeField]
    private ColliderObjectDetector m_playerDetectorToShowEnemyInfos = null;


    private void OnEnable()
    {
        m_enemyState.OnInitialize += OnInitialize;
        m_enemyState.OnDisableEnemy += OnDisableEnemy;

        m_enemyHealth.OnSendHealthPercent += OnSendHealthPercent;
        m_playerDetectorToShowEnemyInfos.OnObjectDetected += OnObjectDetected;
        m_playerDetectorToShowEnemyInfos.OnObjectNotDetectedAnymore += OnObjectNotDetectedAnymore;
    }

    private void OnDisable()
    {
        m_enemyState.OnInitialize -= OnInitialize;
        m_enemyState.OnDisableEnemy -= OnDisableEnemy;

        m_enemyHealth.OnSendHealthPercent -= OnSendHealthPercent;
        m_playerDetectorToShowEnemyInfos.OnObjectDetected -= OnObjectDetected;
        m_playerDetectorToShowEnemyInfos.OnObjectNotDetectedAnymore -= OnObjectNotDetectedAnymore;
    }

    private void OnInitialize()
    {
        m_healthBarObject.SetActive(false);
    }

    private void OnDisableEnemy()
    {
        m_healthBarObject.SetActive(false);
    }

    private void OnObjectDetected(GameObject colliderObject)
    {
        m_healthBarObject.SetActive(true);
    }

    private void OnObjectNotDetectedAnymore(GameObject colliderObject)
    {
        m_healthBarObject.SetActive(false);
    }

    private void OnSendHealthPercent(float healthPercent)
    {
        m_healthBarSlider.value = healthPercent;
    }
}
