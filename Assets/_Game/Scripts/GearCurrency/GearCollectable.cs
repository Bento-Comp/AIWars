using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearCollectable : MonoBehaviour
{
    public static System.Action<float> OnSendCollectedGearValue;

    [SerializeField]
    private GameObject m_rootObject = null;

    [SerializeField]
    private Collider m_triggerCollider = null;

    [SerializeField]
    private float m_smoothTime = 0.035f;

    private Vector3 m_destination;
    private Vector3 m_originPosition;
    private float m_progressionPercentToDestination;
    private float m_spawnHeightParabola;
    private float m_gearValue;
    private bool m_canMove;
    private bool m_destroyOnTargetReached;
    
    private float m_smoothDampVelocity = 0.5f;


    private void OnEnable()
    {
        GearSpawner.OnSpawnGear += OnSpawnGear;
        PlayerGearCollector.OnCollectGear += OnPlayerCollectGear;
    }

    private void OnDisable()
    {
        GearSpawner.OnSpawnGear -= OnSpawnGear;
        PlayerGearCollector.OnCollectGear -= OnPlayerCollectGear;
    }


    private void Update()
    {
        if (m_canMove == false)
            return;

        if (m_progressionPercentToDestination > 1f)
            return;


        m_rootObject.transform.position = MathParabola.Parabola(m_originPosition, m_destination, m_spawnHeightParabola, m_progressionPercentToDestination);

        // V2 of progression
        m_progressionPercentToDestination = Mathf.SmoothDamp(m_progressionPercentToDestination, 1f, ref m_smoothDampVelocity, m_smoothTime);


        if (m_progressionPercentToDestination >= 0.95f)
        {
            m_canMove = false;

            if (m_destroyOnTargetReached)
            {
                Destroy(m_rootObject);
            }
        }
    }


    private void OnPlayerCollectGear(GameObject colliderGameobject, GameObject playerGearCollector)
    {
        if (colliderGameobject != m_rootObject)
            return;

        m_triggerCollider.enabled = false;

        m_originPosition = m_rootObject.transform.position;
        m_destination = playerGearCollector.transform.position;
        m_progressionPercentToDestination = 0f;
        m_canMove = true;
        m_destroyOnTargetReached = true;

        OnSendCollectedGearValue?.Invoke(m_gearValue);
    }

    private void OnSpawnGear(GameObject gearCurrencyReference, Vector3 originPosition, Vector3 spawnDestination, float spawnHeightParabola, float gearValue)
    {
        if (gearCurrencyReference != m_rootObject)
            return;

        m_gearValue = gearValue;
        m_triggerCollider.enabled = true;
        m_spawnHeightParabola = spawnHeightParabola;
        m_originPosition = originPosition;
        m_destination = spawnDestination;
        m_canMove = true;
        m_destroyOnTargetReached = false;
        m_progressionPercentToDestination = 0f;

    }


}
