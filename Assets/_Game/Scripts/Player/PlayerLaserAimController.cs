using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserAimController : MonoBehaviour
{
    [SerializeField]
    private PlayerDetector m_playerDetector = null;

    [SerializeField]
    private LineRenderer m_laserAimLineRenderer = null;

    [SerializeField]
    private GameObject m_laserStartPosition = null;

    


    private Vector3[] m_lineRedererPositions;


    private void Start()
    {
        m_lineRedererPositions = new Vector3[2];
    }

    private void FixedUpdate()
    {
        UpdateLaserAimLineRenderer();
    }


    private void UpdateLaserAimLineRenderer()
    {
        m_lineRedererPositions[0] = m_laserStartPosition.transform.position;

        if (m_playerDetector.NearestObject != null)
        {
            m_lineRedererPositions[1] = m_playerDetector.NearestObject.transform.position + Vector3.up * m_laserStartPosition.transform.position.y;
        }
        else
        {
            m_lineRedererPositions[1] = m_laserStartPosition.transform.position;
        }

        m_laserAimLineRenderer.SetPositions(m_lineRedererPositions);
    }
}
