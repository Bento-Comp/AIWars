using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBump : MonoBehaviour
{
    public System.Action OnStartBump;
    public System.Action OnStopBump;

    [SerializeField]
    private Rigidbody m_body = null;

    [SerializeField]
    private GameObject m_triggerCollider = null;

    [SerializeField]
    private float m_bumpDuration = 0.5f;

    [SerializeField]
    private float m_bumpDistance = 1f;

    [SerializeField]
    private float m_smoothTime = 0.5f;


    private Coroutine m_bumpCoroutine;
    private Vector3 m_bumpDirection;
    private Vector3 m_velocity;
    private float m_bumpTimer;
    private bool m_isBumped;

    public bool IsBumped { get => m_isBumped; }


    private void OnEnable()
    {
        Projectile.OnProjectileHit += OnProjectileHit;
    }

    private void OnDisable()
    {
        Projectile.OnProjectileHit -= OnProjectileHit;
    }


    private void OnProjectileHit(GameObject colliderGameobject, Vector3 sourcePosition, float damage)
    {
        if (colliderGameobject != m_triggerCollider)
            return;

        m_bumpDirection = m_triggerCollider.transform.position - sourcePosition;
        m_bumpDirection.y = 0;
        m_bumpDirection.Normalize();


        if (m_bumpCoroutine != null)
            StopCoroutine(m_bumpCoroutine);

        m_bumpCoroutine = StartCoroutine(BumpCoroutine());
    }


    private IEnumerator BumpCoroutine()
    {
        m_isBumped = true;
        OnStartBump?.Invoke();

        Vector3 bumpFinalTargetPosition = m_body.position + m_bumpDirection * m_bumpDistance;
        m_bumpTimer = 0f;

        while (m_bumpTimer < m_bumpDuration)
        {
            m_bumpTimer += Time.deltaTime;
            m_body.position = Vector3.SmoothDamp(m_body.position, bumpFinalTargetPosition, ref m_velocity, m_smoothTime);
            yield return new WaitForEndOfFrame();
        }

        m_isBumped = false;

        OnStopBump?.Invoke();
    }
}
