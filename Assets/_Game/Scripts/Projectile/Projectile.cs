using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //GameObject : collider hit; Vector3 : source position; float : damage
    public static System.Action<GameObject, Vector3, float> OnProjectileHit;

    [SerializeField]
    private Rigidbody m_body = null;

    [SerializeField]
    private GameObject m_hitSurfaceFxPrefab = null;

    [SerializeField]
    private LayerMask m_effectiveLayer = 0;

    [SerializeField]
    private float m_projectileSpeed = 50f;

    private Vector3 m_sourcePosition;
    private Vector3 m_movementDirection;
    private float m_damage;
    private bool m_isInitialized;



    private void OnTriggerEnter(Collider other)
    {
        if (m_effectiveLayer != (m_effectiveLayer | (1 << other.gameObject.layer)))
            return;


        GameObject hitSurfaceFx = Instantiate(m_hitSurfaceFxPrefab, m_body.position, m_body.rotation);


        OnProjectileHit?.Invoke(other.gameObject, m_sourcePosition, m_damage);
        Destroy(m_body.gameObject);
    }


    private void FixedUpdate()
    {
        Move();
    }


    public void Initialize(Vector3 sourcePosition, Vector3 direction, float lifeTime, float damage)
    {
        m_sourcePosition = sourcePosition;
        SetDirection(direction);
        m_damage = damage;
        m_isInitialized = true;

        Destroy(m_body.gameObject, lifeTime);
    }


    private void Move()
    {
        if (m_isInitialized)
        {
            m_body.transform.position += m_movementDirection * m_projectileSpeed * Time.deltaTime;
        }
    }


    private void SetDirection(Vector3 direction)
    {
        m_movementDirection = direction;

        m_body.transform.forward = direction;
    }

}
