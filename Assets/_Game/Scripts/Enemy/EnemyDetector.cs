using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class EnemyDetector : MonoBehaviour
{
    public System.Action OnEnemyInRange;
    public System.Action OnEnemyOutOfRange;
    

    [SerializeField]
    private EnemyState m_enemyState = null;

    [SerializeField]
    private EnemyMovement m_enemyMovement = null;

    [SerializeField]
    private ColliderObjectDetector m_playerDetector = null;

    [SerializeField]
    private AreaOfDetection m_areaOfDetectionVisual = null;

    [SerializeField]
    private SphereCollider m_playerColliderDetector = null;

    [SerializeField]
    private LaserFxController m_laserFxController = null;

    [SerializeField]
    private Color m_defaultAreaOfDetectionColor = Color.white;

    [SerializeField]
    private Color m_detectedAreaOfDetectionColor = Color.red;

    [SerializeField]
    private LayerMask m_layerToDetect = 0;

    [SerializeField]
    private LayerMask m_playerLayer = 0;

    [SerializeField]
    private Vector3 m_raycastToEnemyOffset = Vector3.up;

    [SerializeField]
    private float m_detectorRange = 5f;


    private List<GameObject> m_detectedObjectList;
    private GameObject m_nearestObjectBuffer;
    private GameObject m_nearestObject;

    public GameObject NearestObject { get => m_nearestObject; }
    public bool HasTarget { get => m_nearestObject != null; }


    private void OnEnable()
    {
        m_enemyState.OnInitialize += OnInitialize;
        m_enemyState.OnDisableEnemy += OnDisableEnemy;

        m_enemyMovement.OnStopChasing += ResetDetector;

        PlayerStateController.OnPlayerDeath += ResetDetector;
        PlayerStateController.OnPlayerAlive += ResetDetector;
    }

    private void OnDisable()
    {
        m_enemyState.OnInitialize -= OnInitialize;
        m_enemyState.OnDisableEnemy -= OnDisableEnemy;

        m_enemyMovement.OnStopChasing -= ResetDetector;

        PlayerStateController.OnPlayerDeath -= ResetDetector;
        PlayerStateController.OnPlayerAlive -= ResetDetector;
    }

    private void Update()
    {
        UpdateDetectionRange();
        CleanupObjectInRange();
    }

    private void FixedUpdate()
    {
        m_nearestObject = GetNearestObject();
    }

    private void OnInitialize()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_playerDetector.OnObjectDetected += OnObjectDetected;
        m_playerDetector.OnObjectNotDetectedAnymore += OnObjectNotDetectedAnymore;

        m_laserFxController?.ToggleLaserFx(false);

        if (m_detectedObjectList == null)
            m_detectedObjectList = new List<GameObject>();
        else
            m_detectedObjectList.Clear();
    }

    private void OnDisableEnemy()
    {
        m_laserFxController?.ToggleLaserFx(false);
        m_areaOfDetectionVisual.UpdateAreaSpriteColor(m_defaultAreaOfDetectionColor);

        m_detectedObjectList.Clear();
        m_playerDetector.OnObjectDetected -= OnObjectDetected;
        m_playerDetector.OnObjectNotDetectedAnymore -= OnObjectNotDetectedAnymore;
    }

    private void ResetDetector()
    {
        m_nearestObject = null;
        m_nearestObjectBuffer = null;
        m_laserFxController?.ToggleLaserFx(false);
        m_areaOfDetectionVisual.UpdateAreaSpriteColor(m_defaultAreaOfDetectionColor);

        
        m_detectedObjectList?.Clear();
    }

    private void OnObjectDetected(GameObject detectedObject)
    {
        m_detectedObjectList.Add(detectedObject);

        if (m_detectedObjectList.Count == 1)
        {
            m_laserFxController?.ToggleLaserFx(true);
            m_nearestObject = GetNearestObject();
            OnEnemyInRange?.Invoke();
        }
    }

    private void OnObjectNotDetectedAnymore(GameObject detectedObject)
    {
        m_detectedObjectList.Remove(detectedObject);

        if (m_detectedObjectList.Count == 0)
        {
            //m_areaOfDetectionVisual.UpdateAreaSpriteColor(m_defaultAreaOfDetectionColor);
            m_laserFxController?.ToggleLaserFx(false);
            m_nearestObject = GetNearestObject();
            OnEnemyOutOfRange?.Invoke();
        }
    }

    private void UpdateDetectionRange()
    {
        if (m_playerColliderDetector != null)
            m_playerColliderDetector.radius = m_detectorRange;

        if (m_areaOfDetectionVisual != null)
            m_areaOfDetectionVisual.UpdateScale(m_detectorRange);
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

                if ((m_playerLayer & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    // RAY TO TARGET VISIBLE
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
                    Debug.DrawLine(transform.position + m_raycastToEnemyOffset + Vector3.up * 0.2f,
m_detectedObjectList[i].transform.position + m_raycastToEnemyOffset + Vector3.up * 0.2f, Color.red, 0.1f);
                }
            }

        }

        if(m_nearestObjectBuffer != null)
        {
            m_areaOfDetectionVisual.UpdateAreaSpriteColor(m_detectedAreaOfDetectionColor);
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

}
