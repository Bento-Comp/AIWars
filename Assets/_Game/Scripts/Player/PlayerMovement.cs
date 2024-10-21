using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private UniButton.TouchButtonController_Stick m_stick = null;

    [SerializeField]
    private Rigidbody m_body = null;

    [SerializeField]
    private PlayerStat_MovementSpeed m_movementSpeedStat = null;


    private Vector3 m_desiredMovement;
    private float m_movementSpeed;
    private bool m_isMovementEnabled;


    public Vector3 DesiredMovement { get => m_desiredMovement; }


    private void OnEnable()
    {
        m_movementSpeedStat.OnStatChange += OnStatChange;
    }

    private void OnDisable()
    {
        m_movementSpeedStat.OnStatChange -= OnStatChange;
    }

    private void Start()
    {
        EnableMovement();
    }

    private void FixedUpdate()
    {
        if (m_stick.gameObject.activeInHierarchy)
            Move(m_stick.Stick);

        DisableVelocity();
    }

    private void DisableVelocity()
    {
        m_body.angularVelocity = Vector3.zero;
        m_body.velocity = Vector3.zero;
    }

    private void EnableMovement()
    {
        m_isMovementEnabled = true;
    }

    private void DisableMovement()
    {
        m_isMovementEnabled = false;
    }

    private void OnStatChange()
    {
        m_movementSpeed = m_movementSpeedStat.GetStatValue();
    }

    private void Move(Vector3 direction)
    {
        if (m_isMovementEnabled)
        {
            m_desiredMovement.x = direction.x;
            m_desiredMovement.z = direction.y;

            m_body.transform.position += m_desiredMovement * m_movementSpeed * Time.deltaTime;
        }
    }
}
