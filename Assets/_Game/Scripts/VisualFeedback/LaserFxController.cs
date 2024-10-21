using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFxController : MonoBehaviour
{
    [SerializeField]
    private LineRenderer m_laserLineRenderer = null;

    [SerializeField]
    private Transform m_laserStartPosition = null;

    [SerializeField]
    private Transform m_laserEndPosition = null;

    [SerializeField]
    private LayerMask m_effectiveLayer = 0;


    private bool m_isEnabled;

    public bool IsEnabled { get => m_isEnabled; }

    private void FixedUpdate()
    {
        UpdateLaserPosition();
    }


    public void ToggleLaserFx(bool state)
    {
        m_isEnabled = state;
        m_laserLineRenderer.gameObject.SetActive(state);
    }


    private void UpdateLaserPosition()
    {
        if (m_isEnabled == false)
            return;

        m_laserLineRenderer.SetPosition(0, m_laserStartPosition.position);

        RaycastHit hit;

        if (Physics.Raycast(m_laserStartPosition.position, m_laserStartPosition.forward, out hit, (m_laserEndPosition.position - m_laserStartPosition.position).magnitude, m_effectiveLayer))
        {
            m_laserLineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            m_laserLineRenderer.SetPosition(1, m_laserEndPosition.position);
        }
    }





}
