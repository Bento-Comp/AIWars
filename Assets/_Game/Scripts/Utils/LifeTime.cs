using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField]
    private GameObject m_controlledObject = null;

    [SerializeField]
    private float m_lifeTime = 1f;

    private float m_timer;

    private void Start()
    {
        m_timer = 0f;
    }

    private void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_lifeTime)
            Destroy(m_controlledObject);
    }
}
