using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyGearValue_UI : MonoBehaviour
{

    [SerializeField]
    private ColliderObjectDetector m_playerDetectorToShowEnemyInfos = null;

    [SerializeField]
    private GameObject m_gearUI = null;

    [SerializeField]
    private TMP_Text m_gearText = null;

    [SerializeField]
    private GearSpawner m_gearSpawner = null;

    [SerializeField]
    private EnemyState m_enemyState = null;


    private void OnEnable()
    {
        m_playerDetectorToShowEnemyInfos.OnObjectDetected += OnObjectDetected;
        m_playerDetectorToShowEnemyInfos.OnObjectNotDetectedAnymore += OnObjectNotDetectedAnymore;

        m_enemyState.OnInitialize += OnInitialize;
        m_enemyState.OnDisableEnemy += OnDisableEnemy;

        m_gearSpawner.OnSendGearValue += OnSendGearValue;
    }

    private void OnDisable()
    {
        m_playerDetectorToShowEnemyInfos.OnObjectDetected -= OnObjectDetected;
        m_playerDetectorToShowEnemyInfos.OnObjectNotDetectedAnymore -= OnObjectNotDetectedAnymore;

        m_enemyState.OnInitialize -= OnInitialize;
        m_enemyState.OnDisableEnemy -= OnDisableEnemy;

        m_gearSpawner.OnSendGearValue -= OnSendGearValue;
    }

    private void OnInitialize()
    {
        m_gearUI.SetActive(false);
    }

    private void OnDisableEnemy()
    {
        m_gearUI.SetActive(false);
    }

    private void OnObjectDetected(GameObject colliderObject)
    {
        m_gearUI.SetActive(true);
    }

    private void OnObjectNotDetectedAnymore(GameObject colliderObject)
    {
        m_gearUI.SetActive(false);
    }

    private void OnSendGearValue(float gearValue)
    {
        m_gearText.text = gearValue.ToString("F0");
    }

}
