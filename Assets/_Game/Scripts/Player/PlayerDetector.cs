using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private ColliderObjectDetector m_enemyDetector = null;

    [SerializeField]
    private ColliderObjectDetector m_shieldEnemyDetector = null;

    [SerializeField]
    private SphereCollider m_enemyColliderDetector = null;

    [SerializeField]
    private Transform m_visualRangeSpriteTransform = null;

    [SerializeField]
    private PlayerStat_ShootRange m_shootRangeStat = null;

    [SerializeField]
    private LayerMask m_layerToDetect = 0;

    [SerializeField]
    private LayerMask m_enemyLayer = 0;

    [SerializeField]
    private Vector3 m_raycastToEnemyOffset = Vector3.up;

    [SerializeField]
    private bool m_enableDebug = false;

    private List<GameObject> m_detectedObjectList;
    private GameObject m_nearestObjectBuffer;
    private GameObject m_nearestObject;

    public GameObject NearestObject { get => m_nearestObject; }
    private float m_detectorRange = 5f;
    public bool HasTarget { get => m_nearestObject != null; }


    private void OnEnable()
    {
        EnemyShield.OnShieldBroken += OnShieldBroken;
        EnemyHealth.OnEnemyKilled += OnEnemyKilled;

        PlayerStateController.OnPlayerDeath += OnPlayerDeath;

        m_shootRangeStat.OnStatChange += OnStatChange;

        m_enemyDetector.OnObjectDetected += OnObjectDetected;
        m_enemyDetector.OnObjectNotDetectedAnymore += OnObjectNotDetectedAnymore;

        m_shieldEnemyDetector.OnObjectDetected += OnObjectDetected;
        m_shieldEnemyDetector.OnObjectNotDetectedAnymore += OnObjectNotDetectedAnymore;
    }

    private void OnDisable()
    {
        EnemyShield.OnShieldBroken -= OnShieldBroken;
        EnemyHealth.OnEnemyKilled -= OnEnemyKilled;

        PlayerHealth.OnPlayerDeath -= OnPlayerDeath;

        m_shootRangeStat.OnStatChange -= OnStatChange;

        m_enemyDetector.OnObjectDetected -= OnObjectDetected;
        m_enemyDetector.OnObjectNotDetectedAnymore -= OnObjectNotDetectedAnymore;

        m_shieldEnemyDetector.OnObjectDetected -= OnObjectDetected;
        m_shieldEnemyDetector.OnObjectNotDetectedAnymore -= OnObjectNotDetectedAnymore;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateDetectionRange();
        CleanupObjectInRange();
        m_nearestObject = GetNearestObject();
    }

    private void OnStatChange()
    {
        m_detectorRange = m_shootRangeStat.GetStatValue();
    }

    private void Initialize()
    {
        m_detectedObjectList = new List<GameObject>();

        UpdateDetectionRange();
    }


    private void OnObjectDetected(GameObject detectedObject)
    {
        m_detectedObjectList.Add(detectedObject);
    }

    private void OnObjectNotDetectedAnymore(GameObject detectedObject)
    {
        m_detectedObjectList.Remove(detectedObject);
    }

    private void UpdateDetectionRange()
    {
        if (m_enemyColliderDetector != null)
            m_enemyColliderDetector.radius = m_detectorRange;

        if (m_visualRangeSpriteTransform != null)
            m_visualRangeSpriteTransform.localScale = Vector3.one * m_detectorRange;
    }


    private GameObject GetNearestObject()
    {
        if (m_detectedObjectList == null || m_detectedObjectList.Count <= 0)
            return null;

        m_nearestObjectBuffer = null;

        float distance = -1f;
        float newDistance = 0f;


        for (int i = 0; i < m_detectedObjectList.Count; i++)
        {
            RaycastHit hit;


            if (Physics.Raycast(transform.position + m_raycastToEnemyOffset,
(m_detectedObjectList[i].transform.position + m_raycastToEnemyOffset) - (transform.position + m_raycastToEnemyOffset)
                , out hit, m_detectorRange, m_layerToDetect))
            {

                if ((m_enemyLayer & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    // RAY TO TARGET VISIBLE
                    if (m_enableDebug)
                        Debug.DrawLine(transform.position + m_raycastToEnemyOffset + Vector3.up * 0.1f,
m_detectedObjectList[i].transform.position + m_raycastToEnemyOffset + Vector3.up * 0.1f, Color.green, 0.1f);

                    newDistance = Vector3.Distance(transform.position, m_detectedObjectList[i].transform.position);

                    if (distance < 0 || newDistance < distance)
                    {
                        m_nearestObjectBuffer = m_detectedObjectList[i];
                        distance = newDistance;
                    }
                }
                else
                {
                    // RAY TO TARGET NOT VISIBLE
                    if (m_enableDebug)
                        Debug.DrawLine(transform.position + m_raycastToEnemyOffset + Vector3.up * 0.2f,
m_detectedObjectList[i].transform.position + m_raycastToEnemyOffset + Vector3.up * 0.2f, Color.red, 0.1f);
                }
            }

        }

        return m_nearestObjectBuffer;
    }

    private void CleanupObjectInRange()
    {
        if (m_detectedObjectList == null || m_detectedObjectList.Count == 0)
            return;

        for (int i = 0; i < m_detectedObjectList.Count; i++)
        {
            if (m_detectedObjectList[i] == null)
                m_detectedObjectList.RemoveAt(i);
        }
    }

    private void OnShieldBroken(GameObject target)
    {
        RemoveEnemyFromDetector(target);
    }

    private void OnEnemyKilled(GameObject target)
    {
        RemoveEnemyFromDetector(target);
    }

    private void OnPlayerDeath()
    {
        m_detectedObjectList.Clear();

        m_nearestObject = null;
    }

    private void RemoveEnemyFromDetector(GameObject enemyReference)
    {
        if (m_nearestObject == enemyReference)
        {
            m_nearestObject = null;
        }

        if (m_detectedObjectList.Contains(enemyReference))
        {
            m_detectedObjectList.Remove(enemyReference);
        }
    }

}
