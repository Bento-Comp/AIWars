using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_body = null;

    [SerializeField]
    private PlayerDetector m_playerDetector = null;

    [SerializeField]
    private PlayerMovement m_playerMovement = null;

    [SerializeField]
    private float m_rotationSpeed = 40f;

    private Vector3 m_targetDirectionBuffer;
    private Vector3 m_calculatedDirection;


    private void Update()
    {
        AimTowardsObject();
    }


    private void AimTowardsObject()
    {
        if (m_playerDetector.NearestObject != null)
            m_targetDirectionBuffer = m_playerDetector.NearestObject.transform.position - m_body.position;
        else
            m_targetDirectionBuffer = (m_body.transform.position + m_playerMovement.DesiredMovement) - m_body.transform.position;


        m_calculatedDirection = Vector3.RotateTowards(m_body.transform.forward, m_targetDirectionBuffer, m_rotationSpeed * Time.deltaTime, 0.0f);

        m_body.rotation = Quaternion.LookRotation(m_calculatedDirection);
    }
}
