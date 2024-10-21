using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Floor : MonoBehaviour
{
    [SerializeField]
    private Transform m_controlledTransform = null;

    [SerializeField]
    private Vector3 m_desiredScale = Vector3.one;

    private void Update()
    {
        if (m_controlledTransform == null)
            return;

        m_controlledTransform.localScale = m_desiredScale;
    }

}
